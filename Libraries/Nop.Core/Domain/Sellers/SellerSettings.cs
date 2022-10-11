using Nop.Core.Configuration;

namespace Nop.Core.Domain.Sellers
{
    /// <summary>
    /// Seller settings
    /// </summary>
    public class SellerSettings : ISettings
    {
        /// <summary>
        /// Gets or sets the default value to use for Seller page size options (for new sellers)
        /// </summary>
        public string DefaultSellerPageSizeOptions { get; set; }

        /// <summary>
        /// Gets or sets the value indicating how many sellers to display in sellers block
        /// </summary>
        public int SellersBlockItemsToDisplay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display vendor name on the product details page
        /// </summary>
        public bool ShowSellerOnProductDetailsPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to display vendor name on the order details page
        /// </summary>
        public bool ShowSellerOnOrderDetailsPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers can contact sellers
        /// </summary>
        public bool AllowCustomersToContactSellers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether users can fill a form to become a new vendor
        /// </summary>
        public bool AllowCustomersToApplyForSellerAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sellers have to accept terms of service during registration
        /// </summary>
        public bool TermsOfServiceEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether it is possible to carry out advanced search in the store by vendor
        /// </summary>
        public bool AllowSearchBySeller { get; set; }

        /// <summary>
        /// Get or sets a value indicating whether vendor can edit information about itself (public store)
        /// </summary>
        public bool AllowSellersToEditInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the store owner is notified that the vendor information has been changed
        /// </summary>
        public bool NotifyStoreOwnerAboutSellerInformationChange { get; set; }

        /// <summary>
        /// Gets or sets a maximum number of products per vendor
        /// </summary>
        public int MaximumProductNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sellers are allowed to import products
        /// </summary>
        public bool AllowSellersToImportProducts { get; set; }
    }
}
