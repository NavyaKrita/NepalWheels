﻿using Nop.Core.Domain.Localization;

namespace Nop.Core.Domain.Sellers
{
    /// <summary>
    /// Represents a vendor attribute value
    /// </summary>
    public partial class SellerAttributeValue : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value is pre-selected
        /// </summary>
        public bool IsPreSelected { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the vendor attribute identifier
        /// </summary>
        public int SellerAttributeId { get; set; }
    }
}