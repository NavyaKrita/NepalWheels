using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Sellers;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents seller attribute extensions
    /// </summary>
    public static class SellerAttributeExtensions
    {
        /// <summary>
        /// A value indicating whether this seller attribute should have values
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>True if the attribute should have values; otherwise false</returns>
        public static bool ShouldHaveValues(this SellerAttribute sellerAttribute)
        {
            if (sellerAttribute == null)
                return false;

            if (sellerAttribute.AttributeControlType == AttributeControlType.TextBox ||
                sellerAttribute.AttributeControlType == AttributeControlType.MultilineTextbox ||
                sellerAttribute.AttributeControlType == AttributeControlType.Datepicker ||
                sellerAttribute.AttributeControlType == AttributeControlType.FileUpload)
                return false;

            //other attribute control types support values
            return true;
        }
    }
}