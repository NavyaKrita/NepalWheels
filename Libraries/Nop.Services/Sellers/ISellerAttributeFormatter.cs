using System.Threading.Tasks;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute formatter
    /// </summary>
    public partial interface ISellerAttributeFormatter
    {
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
        Task<string> FormatAttributesAsync(string attributesXml, string separator = "<br />", bool htmlEncode = true);
    }
}