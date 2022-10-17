using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Models.Sellers
{
    public record NkSellerLocationModel : BaseNopEntityModel
    {
        public NkSellerLocationModel()
        {

        }

        [NopResourceDisplayName("NkSellerLocation.Fields.Manufacturer")]
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        public int CategoryId { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.Address")]
        public string Address { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.Genre")]
        public string Genre { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.MapLocation")]
        public string MapLocation { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.Published")]
        public bool Published { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.Deleted")]
        public bool Deleted { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.DistrictName")]
        public string DistrictName { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.ManufacturerName")]
        public string ManufacturerName { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.SeName")]
        public string SeName { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.StateName")]
        public string StateName { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.Email")]
        public string Email { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.DealerName")]
        public string DealerName { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.ContactPerson")]
        public string ContactPerson { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.PhoneNo")]
        public string PhoneNo { get; set; }
        [NopResourceDisplayName("NkSellerLocation.Fields.MobileNo")]
        public string MobileNo { get; set; }
    }

}
