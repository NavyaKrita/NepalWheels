using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Sellers;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute parser
    /// </summary>
    public partial interface ISellerAttributeParser
    {
        /// <summary>
        /// Gets seller attributes from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of vendor attributes
        /// </returns>
        Task<IList<VendorAttribute>> ParseSellerAttributesAsync(string attributesXml);

        /// <summary>
        /// Get seller attribute values from XML
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of vendor attribute values
        /// </returns>
        Task<IList<VendorAttributeValue>> ParseSellerAttributeValuesAsync(string attributesXml);

        /// <summary>
        /// Gets values of the selected seller attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="sellerAttributeId">Vendor attribute identifier</param>
        /// <returns>Values of the vendor attribute</returns>
        IList<string> ParseValues(string attributesXml, int sellerAttributeId);

        /// <summary>
        /// Adds a seller attribute
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <param name="sellerAttribute">Vendor attribute</param>
        /// <param name="value">Value</param>
        /// <returns>Attributes in XML format</returns>sellerAttribute
        string AddSellerAttribute(string attributesXml, VendorAttribute sellerAttribute, string value);

        /// <summary>
        /// Validates seller attributes
        /// </summary>
        /// <param name="attributesXml">Attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the warnings
        /// </returns>
        Task<IList<string>> GetAttributeWarningsAsync(string attributesXml);
    }
}