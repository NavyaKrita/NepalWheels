using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    public record NkVendorLocationModel : BaseNopEntityModel
    {
        public NkVendorLocationModel()
        {
            Locales = new List<NkVendorLocationLocalizedModel>();
        }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Manufacturer")]
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Address")]
        public string Address { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Genre")]
        public int Genre { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.MapLocation")]
        public string MapLocation { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Published")]
        public bool Published { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Deleted")]
        public bool Deleted { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.DistrictName")]
        public string DistrictName { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.ManufacturerName")]
        public string ManufacturerName { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.SeName")]
        public string SeName { get;  set; }

      
        public IList<NkVendorLocationLocalizedModel> Locales { get; set; }
    }
    public partial record NkVendorLocationLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.MapLocation")]
        public string MapLocation { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.SeName")]
        public string SeName { get; set; }
    }
}
