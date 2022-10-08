using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Caching;
using Nop.Core.Domain.Sellers;
using Nop.Data;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute service implementation
    /// </summary>
    public partial class SellerAttributeService : ISellerAttributeService
    {
        #region Fields

        private readonly IRepository<SellerAttribute> _sellerAttributeRepository;
        private readonly IRepository<SellerAttributeValue> _sellerAttributeValueRepository;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public SellerAttributeService(IRepository<SellerAttribute> sellerAttributeRepository,
            IRepository<SellerAttributeValue> sellerAttributeValueRepository,
            IStaticCacheManager staticCacheManager)
        {
            _sellerAttributeRepository = sellerAttributeRepository;
            _sellerAttributeValueRepository = sellerAttributeValueRepository;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Methods

        #region Seller attributes

        /// <summary>
        /// Gets all seller attributes
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attributes
        /// </returns>
        public virtual async Task<IList<SellerAttribute>> GetAllSellerAttributesAsync()
        {
            return await _sellerAttributeRepository.GetAllAsync(
                query => query.OrderBy(sellerAttribute => sellerAttribute.DisplayOrder).ThenBy(sellerAttribute => sellerAttribute.Id),
                cache => default);
        }

        /// <summary>
        /// Gets a seller attribute 
        /// </summary>
        /// <param name="sellerAttributeId">Seller attribute identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attribute
        /// </returns>
        public virtual async Task<SellerAttribute> GetSellerAttributeByIdAsync(int sellerAttributeId)
        {
            return await _sellerAttributeRepository.GetByIdAsync(sellerAttributeId, cache => default);
        }

        /// <summary>
        /// Inserts a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertSellerAttributeAsync(SellerAttribute sellerAttribute)
        {
            await _sellerAttributeRepository.InsertAsync(sellerAttribute);
        }

        /// <summary>
        /// Updates a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateSellerAttributeAsync(SellerAttribute sellerAttribute)
        {
            await _sellerAttributeRepository.UpdateAsync(sellerAttribute);
        }

        /// <summary>
        /// Deletes a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSellerAttributeAsync(SellerAttribute sellerAttribute)
        {
            await _sellerAttributeRepository.DeleteAsync(sellerAttribute);
        }

        #endregion

        #region Seller attribute values

        /// <summary>
        /// Gets seller attribute values by seller attribute identifier
        /// </summary>
        /// <param name="sellerAttributeId">The seller attribute identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attribute values
        /// </returns>
        public virtual async Task<IList<SellerAttributeValue>> GetSellerAttributeValuesAsync(int sellerAttributeId)
        {
            var key = _staticCacheManager.PrepareKeyForDefaultCache(NopSellerDefaults.SellerAttributeValuesByAttributeCacheKey, sellerAttributeId);

            var query = _sellerAttributeValueRepository.Table
                .Where(sellerAttributeValue => sellerAttributeValue.SellerAttributeId == sellerAttributeId)
                .OrderBy(sellerAttributeValue => sellerAttributeValue.DisplayOrder)
                .ThenBy(sellerAttributeValue => sellerAttributeValue.Id);

            return await _staticCacheManager.GetAsync(key, async () => await query.ToListAsync());
        }

        /// <summary>
        /// Gets a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValueId">Seller attribute value identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attribute value
        /// </returns>
        public virtual async Task<SellerAttributeValue> GetSellerAttributeValueByIdAsync(int sellerAttributeValueId)
        {
            return await _sellerAttributeValueRepository.GetByIdAsync(sellerAttributeValueId, cache => default);
        }

        /// <summary>
        /// Inserts a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Seller attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertSellerAttributeValueAsync(SellerAttributeValue sellerAttributeValue)
        {
            await _sellerAttributeValueRepository.InsertAsync(sellerAttributeValue);
        }

        /// <summary>
        /// Updates the seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Seller attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateSellerAttributeValueAsync(SellerAttributeValue sellerAttributeValue)
        {
            await _sellerAttributeValueRepository.UpdateAsync(sellerAttributeValue);
        }

        /// <summary>
        /// Deletes a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Seller attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSellerAttributeValueAsync(SellerAttributeValue sellerAttributeValue)
        {
            await _sellerAttributeValueRepository.DeleteAsync(sellerAttributeValue);
        }

        #endregion

        #endregion
    }
}