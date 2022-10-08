using Nop.Core.Domain.Sellers;
using Nop.Services.Caching;
using System.Threading.Tasks;

namespace Nop.Services.Sellers.Caching
{
    /// <summary>
    /// Represents a seller attribute value cache event consumer
    /// </summary>
    public partial class SellerAttributeValueCacheEventConsumer : CacheEventConsumer<SellerAttributeValue>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(SellerAttributeValue entity)
        {
            await RemoveAsync(NopSellerDefaults.SellerAttributeValuesByAttributeCacheKey, entity.SellerAttributeId);
        }
    }
}
