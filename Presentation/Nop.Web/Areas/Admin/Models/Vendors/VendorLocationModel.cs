using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Vendors;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    public record NkVendorLocationModel : BaseNopEntityModel
    {
        /// </summary>
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Manufacturer")]
        public int ManufacturerId { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.DistrictName")]
        public int DistrictId { get; set; }
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        /// 
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Address")]
        public string Address { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Email")]
        public string Email { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Phone")]
        public string PhoneNo { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Genre")]
        public int Genre { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.MapLocation")]
        public string Maplocation { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Published")]
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.DealerName")]

        public int CategoryId { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.DealerName")]
        public string DealerName { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.ContactPerson")]
        public string ContactPerson { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.MobileNo")]
        public string MobileNo { get; set; }

        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.DistrictName")]
        public string DistrictName { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.ManufacturerName")]
        public string ManufacturerName { get; set; }

        public string GenreName { get; set; }



    }


}
