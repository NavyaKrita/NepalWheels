using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Sellers;
using Nop.Core.Html;
using Nop.Data;
using Nop.Services.Sellers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class SellerService : ISellerService
    {
        #region Fields

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Seller> _sellerRepository;
        private readonly IRepository<SellerNote> _sellerNoteRepository;
       
        #endregion

        #region Ctor

        public SellerService(IRepository<Customer> customerRepository,
            IRepository<Product> productRepository,
            IRepository<Seller> sellerRepository,
            IRepository<SellerNote> sellerNoteRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _sellerRepository = sellerRepository;
            _sellerNoteRepository = sellerNoteRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a vendor by vendor identifier
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        public virtual async Task<Seller> GetSellerByIdAsync(int vendorId)
        {
            return await _sellerRepository.GetByIdAsync(vendorId, cache => default);
        }

        /// <summary>
        /// Gets a vendor by product identifier
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor
        /// </returns>
        public virtual async Task<Seller> GetSellerByProductIdAsync(int productId)
        {
            if (productId == 0)
                return null;

            return await (from v in _sellerRepository.Table
                    join p in _productRepository.Table on v.Id equals p.VendorId
                    where p.Id == productId
                    select v).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets sellers by product identifiers
        /// </summary>
        /// <param name="productIds">Array of product identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sellers
        /// </returns>
        public virtual async Task<IList<Seller>> GetSellersByProductIdsAsync(int[] productIds)
        {
            if (productIds is null)
                throw new ArgumentNullException(nameof(productIds));

            return await (from v in _sellerRepository.Table
                    join p in _productRepository.Table on v.Id equals p.VendorId
                    where productIds.Contains(p.Id) && !v.Deleted && v.Active
                    select v).Distinct().ToListAsync();
        }

        /// <summary>
        /// Gets a sellers by customers identifiers
        /// </summary>
        /// <param name="customerIds">Array of customer identifiers</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sellers
        /// </returns>
        public virtual async Task<IList<Seller>> GetSellersByCustomerIdsAsync(int[] customerIds)
        {
            if (customerIds is null)
                throw new ArgumentNullException(nameof(customerIds));

            return await (from v in _sellerRepository.Table
                join c in _customerRepository.Table on v.Id equals c.SellerId
                where customerIds.Contains(c.Id) && !v.Deleted && v.Active
                select v).Distinct().ToListAsync();
        }

        /// <summary>
        /// Delete a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSellerAsync(Seller seller)
        {
            await _sellerRepository.DeleteAsync(seller);
        }

        /// <summary>
        /// Gets all sellers
        /// </summary>
        /// <param name="name">Vendor name</param>
        /// <param name="email">Vendor email</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the sellers
        /// </returns>
        public virtual async Task<IPagedList<Seller>> GetAllSellersAsync(string name = "", string email = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var sellers = await _sellerRepository.GetAllPagedAsync(query =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(v => v.Name.Contains(name));

                if (!string.IsNullOrWhiteSpace(email))
                    query = query.Where(v => v.Email.Contains(email));

                if (!showHidden)
                    query = query.Where(v => v.Active);

                query = query.Where(v => !v.Deleted);
                query = query.OrderBy(v => v.DisplayOrder).ThenBy(v => v.Name).ThenBy(v => v.Email);

                return query;
            }, pageIndex, pageSize);

            return sellers;
        }

        /// <summary>
        /// Inserts a vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertSellerAsync(Seller seller)
        {
            await _sellerRepository.InsertAsync(seller);
        }

        /// <summary>
        /// Updates the vendor
        /// </summary>
        /// <param name="vendor">Vendor</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task UpdateSellerAsync(Seller seller)
        {
            await _sellerRepository.UpdateAsync(seller);
        }

        /// <summary>
        /// Gets a vendor note
        /// </summary>
        /// <param name="vendorNoteId">The vendor note identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor note
        /// </returns>
        public virtual async Task<SellerNote> GetSellerNoteByIdAsync(int vendorNoteId)
        {
            return await _sellerNoteRepository.GetByIdAsync(vendorNoteId, cache => default);
        }

        /// <summary>
        /// Gets all vendor notes
        /// </summary>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor notes
        /// </returns>
        public virtual async Task<IPagedList<SellerNote>> GetSellerNotesBySellerAsync(int vendorId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _sellerNoteRepository.Table.Where(vn => vn.VendorId == vendorId);

            query = query.OrderBy(v => v.CreatedOnUtc).ThenBy(v => v.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Deletes a vendor note
        /// </summary>
        /// <param name="vendorNote">The vendor note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task DeleteSellerNoteAsync(SellerNote vendorNote)
        {
            await _sellerNoteRepository.DeleteAsync(vendorNote);
        }

        /// <summary>
        /// Inserts a vendor note
        /// </summary>
        /// <param name="vendorNote">Vendor note</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task InsertSellerNoteAsync(SellerNote vendorNote)
        {
            await _sellerNoteRepository.InsertAsync(vendorNote);
        }

        /// <summary>
        /// Formats the vendor note text
        /// </summary>
        /// <param name="vendorNote">Vendor note</param>
        /// <returns>Formatted text</returns>
        public virtual string FormatSellerNoteText(SellerNote vendorNote)
        {
            if (vendorNote == null)
                throw new ArgumentNullException(nameof(vendorNote));

            var text = vendorNote.Note;

            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = HtmlHelper.FormatText(text, false, true, false, false, false, false);

            return text;
        }


        #endregion
    }
}