using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Models.Vendors
{
    public record NkVendorLocationModel : BaseNopEntityModel
    {
        public NkVendorLocationModel()
        {

        }

        [NopResourceDisplayName("NkVendorLocation.Fields.Manufacturer")]
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        public int CategoryId { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.Address")]
        public string Address { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.Genre")]
        public string Genre { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.MapLocation")]
        public string MapLocation { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.Published")]
        public bool Published { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.Deleted")]
        public bool Deleted { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.DistrictName")]
        public string DistrictName { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.ManufacturerName")]
        public string ManufacturerName { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.SeName")]
        public string SeName { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.StateName")]
        public string StateName { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.Email")]
        public string Email { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.DealerName")]
        public string DealerName { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.ContactPerson")]
        public string ContactPerson { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.PhoneNo")]
        public string PhoneNo { get; set; }
        [NopResourceDisplayName("NkVendorLocation.Fields.MobileNo")]
        public string MobileNo { get; set; }
    }

}
