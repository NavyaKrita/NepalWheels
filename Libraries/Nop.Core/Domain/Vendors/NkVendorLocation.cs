using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using System;

namespace Nop.Core.Domain.Vendors
{
    /// <summary>
    /// Represents a vendor
    /// </summary>
    public partial class NkVendorLocation : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int Genre { get; set; }
        public string Maplocation { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int CategoryId { get; set; }
        public string DealerName { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }

    }
}
