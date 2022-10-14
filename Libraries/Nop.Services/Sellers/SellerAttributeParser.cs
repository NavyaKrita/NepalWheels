using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml;
using Nop.Core.Domain.Sellers;
using Nop.Core.Domain.Vendors;
using Nop.Services.Localization;
using Nop.Services.Sellers;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute parser implementation
    /// </summary>
    public partial class SellerAttributeParser : ISellerAttributeParser
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISellerAttributeService _sellerAttributeService;

        #endregion

        #region Ctor

        public SellerAttributeParser(ILocalizationService localizationService,
            ISellerAttributeService sellerAttributeService)
        {
            _localizationService = localizationService;
            _sellerAttributeService = sellerAttributeService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets seller attribute identifiers
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>List of seller attribute identifiers</returns>
        protected virtual IList<int> ParseSellerAttributeIds(string attributesXml)
        {
            var ids = new List<int>();
            if (string.IsNullOrEmpty(attributesXml))
                return ids;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                foreach (XmlNode node in xmlDoc.SelectNodes(@"//Attributes/SellerAttribute"))
                {
                    if (node.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node.Attributes["ID"].InnerText.Trim();
                    if (int.TryParse(str1, out var id))
                    {
                        ids.Add(id);
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return ids;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets seller attributes from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of seller attributes
        /// </returns>
        public virtual async Task<IList<VendorAttribute>> ParseSellerAttributesAsync(string attributesXml)
        {
            var result = new List<VendorAttribute>();
            if (string.IsNullOrEmpty(attributesXml))
                return result;

            var ids = ParseSellerAttributeIds(attributesXml);
            foreach (var id in ids)
            {
                var attribute = await _sellerAttributeService.GetSellerAttributeByIdAsync(id);
                if (attribute != null)
                {
                    result.Add(attribute);
                }
            }

            return result;
        }

        /// <summary>
        /// Get seller attribute values from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of seller attribute values
        /// </returns>
        public virtual async Task<IList<VendorAttributeValue>> ParseSellerAttributeValuesAsync(string attributesXml)
        {
            var values = new List<VendorAttributeValue>();
            if (string.IsNullOrEmpty(attributesXml))
                return values;

            var attributes = await ParseSellerAttributesAsync(attributesXml);
            foreach (var attribute in attributes)
            {
                if (!attribute.ShouldHaveValues())
                    continue;

                var valuesStr = ParseValues(attributesXml, attribute.Id);
                foreach (var valueStr in valuesStr)
                {
                    if (string.IsNullOrEmpty(valueStr)) 
                        continue;

                    if (!int.TryParse(valueStr, out var id)) 
                        continue;

                    var value = await _sellerAttributeService.GetSellerAttributeValueByIdAsync(id);
                    if (value != null)
                        values.Add(value);
                }
            }

            return values;
        }

        /// <summary>
        /// Gets values of the selected seller attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="sellerAttributeId">Seller attribute identifier</param>
        /// <returns>Values of the seller attribute</returns>
        public virtual IList<string> ParseValues(string attributesXml, int sellerAttributeId)
        {
            var selectedSellerAttributeValues = new List<string>();
            if (string.IsNullOrEmpty(attributesXml))
                return selectedSellerAttributeValues;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(attributesXml);

                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/SellerAttribute");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node1.Attributes["ID"].InnerText.Trim();
                    if (!int.TryParse(str1, out var id)) 
                        continue;

                    if (id != sellerAttributeId) 
                        continue;

                    var nodeList2 = node1.SelectNodes(@"SellerAttributeValue/Value");
                    foreach (XmlNode node2 in nodeList2)
                    {
                        var value = node2.InnerText.Trim();
                        selectedSellerAttributeValues.Add(value);
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return selectedSellerAttributeValues;
        }

        /// <summary>
        /// Adds a seller attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes in XML format</returns>
        public virtual string AddSellerAttribute(string attributesXml, VendorAttribute sellerAttribute, string value)
        {
            var result = string.Empty;
            try
            {
                var xmlDoc = new XmlDocument();
                if (string.IsNullOrEmpty(attributesXml))
                {
                    var element1 = xmlDoc.CreateElement("Attributes");
                    xmlDoc.AppendChild(element1);
                }
                else
                {
                    xmlDoc.LoadXml(attributesXml);
                }

                var rootElement = (XmlElement)xmlDoc.SelectSingleNode(@"//Attributes");

                XmlElement attributeElement = null;
                //find existing
                var nodeList1 = xmlDoc.SelectNodes(@"//Attributes/SellerAttribute");
                foreach (XmlNode node1 in nodeList1)
                {
                    if (node1.Attributes?["ID"] == null) 
                        continue;

                    var str1 = node1.Attributes["ID"].InnerText.Trim();
                    if (!int.TryParse(str1, out var id)) 
                        continue;

                    if (id != sellerAttribute.Id) 
                        continue;

                    attributeElement = (XmlElement)node1;
                    break;
                }

                //create new one if not found
                if (attributeElement == null)
                {
                    attributeElement = xmlDoc.CreateElement("SellerAttribute");
                    attributeElement.SetAttribute("ID", sellerAttribute.Id.ToString());
                    rootElement.AppendChild(attributeElement);
                }

                var attributeValueElement = xmlDoc.CreateElement("SellerAttributeValue");
                attributeElement.AppendChild(attributeValueElement);

                var attributeValueValueElement = xmlDoc.CreateElement("Value");
                attributeValueValueElement.InnerText = value;
                attributeValueElement.AppendChild(attributeValueValueElement);

                result = xmlDoc.OuterXml;
            }
            catch (Exception exc)
            {
                Debug.Write(exc.ToString());
            }

            return result;
        }

        /// <summary>
        /// Validates seller attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the warnings
        /// </returns>
        public virtual async Task<IList<string>> GetAttributeWarningsAsync(string attributesXml)
        {
            var warnings = new List<string>();

            //ensure it's our attributes
            var attributes1 = await ParseSellerAttributesAsync(attributesXml);

            //validate required seller attributes (whether they're chosen/selected/entered)
            var attributes2 = await _sellerAttributeService.GetAllSellerAttributesAsync();
            foreach (var a2 in attributes2)
            {
                if (!a2.IsRequired) 
                    continue;

                var found = false;
                //selected seller attributes
                foreach (var a1 in attributes1)
                {
                    if (a1.Id != a2.Id) 
                        continue;

                    var valuesStr = ParseValues(attributesXml, a1.Id);
                    foreach (var str1 in valuesStr)
                    {
                        if (string.IsNullOrEmpty(str1.Trim())) 
                            continue;

                        found = true;
                        break;
                    }
                }
                
                if (found) 
                    continue;

                //if not found
                var notFoundWarning = string.Format(await _localizationService.GetResourceAsync("ShoppingCart.SelectAttribute"), await _localizationService.GetLocalizedAsync(a2, a => a.Name));

                warnings.Add(notFoundWarning);
            }

            return warnings;
        }
       
        #endregion
    }
}