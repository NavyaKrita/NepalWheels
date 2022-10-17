using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Html;
using Nop.Services.Localization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute formatter implementation
    /// </summary>
    public partial class SellerAttributeFormatter : ISellerAttributeFormatter
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISellerAttributeParser _sellerAttributeParser;
        private readonly ISellerAttributeService _sellerAttributeService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public SellerAttributeFormatter(ILocalizationService localizationService,
            ISellerAttributeParser sellerAttributeParser,
            ISellerAttributeService sellerAttributeService,
            IWorkContext workContext)
        {
            _localizationService = localizationService;
            _sellerAttributeParser = sellerAttributeParser;
            _sellerAttributeService = sellerAttributeService;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format seller attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="separator">Separator</param>
        /// <param name="htmlEncode">A value indicating whether to encode (HTML) values</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the formatted attributes
        /// </returns>
        public virtual async Task<string> FormatAttributesAsync(string attributesXml, string separator = "<br />", bool htmlEncode = true)
        {
            var result = new StringBuilder();

            var attributes = await _sellerAttributeParser.ParseSellerAttributesAsync(attributesXml);
            for (var i = 0; i < attributes.Count; i++)
            {
                var attribute = attributes[i];
                var valuesStr = _sellerAttributeParser.ParseValues(attributesXml, attribute.Id);
                for (var j = 0; j < valuesStr.Count; j++)
                {
                    var valueStr = valuesStr[j];
                    var formattedAttribute = string.Empty;
                    if (!attribute.ShouldHaveValues())
                    {
                        //no values
                        if (attribute.AttributeControlType == AttributeControlType.MultilineTextbox)
                        {
                            //multiline textbox
                            var attributeName = await _localizationService.GetLocalizedAsync(attribute, a => a.Name, (await _workContext.GetWorkingLanguageAsync()).Id);
                            //encode (if required)
                            if (htmlEncode)
                                attributeName = WebUtility.HtmlEncode(attributeName);
                            formattedAttribute = $"{attributeName}: {HtmlHelper.FormatText(valueStr, false, true, false, false, false, false)}";
                            //we never encode multiline textbox input
                        }
                        else if (attribute.AttributeControlType == AttributeControlType.FileUpload)
                        {
                            //file upload
                            //not supported for vendor attributes
                        }
                        else
                        {
                            //other attributes (textbox, datepicker)
                            formattedAttribute = $"{await _localizationService.GetLocalizedAsync(attribute, a => a.Name, (await _workContext.GetWorkingLanguageAsync()).Id)}: {valueStr}";
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                        }
                    }
                    else
                    {
                        if (int.TryParse(valueStr, out var attributeValueId))
                        {
                            var attributeValue = await _sellerAttributeService.GetSellerAttributeValueByIdAsync(attributeValueId);
                            if (attributeValue != null)
                            {
                                formattedAttribute = $"{await _localizationService.GetLocalizedAsync(attribute, a => a.Name, (await _workContext.GetWorkingLanguageAsync()).Id)}: {await _localizationService.GetLocalizedAsync(attributeValue, a => a.Name, (await _workContext.GetWorkingLanguageAsync()).Id)}";
                            }
                            //encode (if required)
                            if (htmlEncode)
                                formattedAttribute = WebUtility.HtmlEncode(formattedAttribute);
                        }
                    }

                    if (string.IsNullOrEmpty(formattedAttribute))
                        continue;

                    if (i != 0 || j != 0)
                        result.Append(separator);
                    result.Append(formattedAttribute);
                }
            }

            return result.ToString();
        }

        #endregion
    }
}