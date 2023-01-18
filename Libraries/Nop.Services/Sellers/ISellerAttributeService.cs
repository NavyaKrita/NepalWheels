using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core.Domain.Sellers;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Represents a seller attribute service
    /// </summary>
    public partial interface ISellerAttributeService
    {
        #region Seller attributes

        /// <summary>
        /// Gets all seller attributes
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attributes
        /// </returns>
        Task<IList<VendorAttribute>> GetAllSellerAttributesAsync();

        /// <summary>
        /// Gets a seller attribute 
        /// </summary>
        /// <param name="sellerAttributeId">Vendor attribute identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attribute
        /// </returns>
        Task<VendorAttribute> GetSellerAttributeByIdAsync(int sellerAttributeId);

        /// <summary>
        /// Inserts a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertSellerAttributeAsync(VendorAttribute sellerAttribute);

        /// <summary>
        /// Updates a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateSellerAttributeAsync(VendorAttribute sellerAttribute);

        /// <summary>
        /// Deletes a seller attribute
        /// </summary>
        /// <param name="sellerAttribute">Seller attribute</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteSellerAttributeAsync(VendorAttribute sellerAttribute);

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
        Task<IList<VendorAttributeValue>> GetSellerAttributeValuesAsync(int sellerAttributeId);

        /// <summary>
        /// Gets a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValueId">Vendor attribute value identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller attribute value
        /// </returns>
        Task<VendorAttributeValue> GetSellerAttributeValueByIdAsync(int sellerAttributeValueId);

        /// <summary>
        /// Inserts a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Vendor attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertSellerAttributeValueAsync(VendorAttributeValue sellerAttributeValue);

        /// <summary>
        /// Updates a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Vendor attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateSellerAttributeValueAsync(VendorAttributeValue sellerAttributeValue);

        /// <summary>
        /// Deletes a seller attribute value
        /// </summary>
        /// <param name="sellerAttributeValue">Vendor attribute value</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteSellerAttributeValueAsync(VendorAttributeValue sellerAttributeValue);

        #endregion
    }
}