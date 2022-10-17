using Nop.Core.Caching;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents default values related to seller services
    /// </summary>
    public static partial class NopSellerDefaults
    {
        /// <summary>
        /// Gets a generic attribute key to store seller additional info
        /// </summary>
        public static string SellerAttributes => "SellerAttributes";

        /// <summary>
        /// Gets default prefix for seller
        /// </summary>
        public static string SellerAttributePrefix => "seller_attribute_";

        #region Caching defaults

        /// <summary>
        /// Gets a key for caching seller attribute values of the vendor attribute
        /// </summary>
        /// <remarks>
        /// {0} : seller attribute ID
        /// </remarks>
        public static CacheKey SellerAttributeValuesByAttributeCacheKey => new CacheKey("Nop.sellerattributevalue.byattribute.{0}");

        #endregion
    }
}