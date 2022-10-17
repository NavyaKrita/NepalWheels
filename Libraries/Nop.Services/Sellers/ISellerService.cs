using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Sellers;
using Nop.Core.Domain.Vendors;

namespace Nop.Services.Sellers
{
    /// <summary>
    /// Vendor service interface
    /// </summary>
    public partial interface ISellerService
    {
        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        Task<Vendor> GetSellerByIdAsync(int vendorId);

        /// <summary>
        /// Gets a vendors by product identifiers
        /// </summary>
        /// <param name="productIds">Array of product identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        Task<IList<Vendor>> GetSellersByProductIdsAsync(int[] productIds);

        /// <summary>
        /// Gets a vendors by customers identifiers
        /// </summary>
        /// <param name="customerIds">Array of customer identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        Task<IList<Vendor>> GetSellersByCustomerIdsAsync(int[] customerIds);

        /// <summary>
        /// Gets a vendor by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        Task<Vendor> GetSellerByProductIdAsync(int productId);

        /// <summary>
        /// Delete a seller
        /// </summary>
        /// <param name="seller">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteSellerAsync(Vendor seller);

        /// <summary>
        /// Gets all vendors
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="email">Vendor email</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendors
        /// </returns>
        Task<IPagedList<Vendor>> GetAllSellersAsync(string name = "", string email = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertSellerAsync(Vendor seller);

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task UpdateSellerAsync(Vendor seller);

        /// <summary>
        /// Gets a vendor note
        /// </summary>
        /// <param name="sellerNoteId">The vendor note identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor note
        /// </returns>
        Task<VendorNote> GetSellerNoteByIdAsync(int sellerNoteId);

        /// <summary>
        /// Gets all vendor notes
        /// </summary>
        /// <param name="sellerId">Vendor identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor notes
        /// </returns>
        Task<IPagedList<VendorNote>> GetSellerNotesBySellerAsync(int sellerId, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Deletes a seller note
        /// </summary>
        /// <param name="sellerNote">The seller note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task DeleteSellerNoteAsync(VendorNote sellerNote);

        /// <summary>
        /// Inserts a seller note
        /// </summary>
        /// <param name="sellerNote">Vendor note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task InsertSellerNoteAsync(VendorNote sellerNote);

        /// <summary>
        /// Formats the seller note text
        /// </summary>
        /// <param name="sellerNote">Vendor note</param>
        /// <returns>Formatted text</returns>
        string FormatSellerNoteText(VendorNote sellerNote);
       
    }
}