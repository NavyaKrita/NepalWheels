using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    public partial record CreateVendorLocationModel : NkVendorLocationModel
    {
        public CreateVendorLocationModel()
        {
            Locales = new List<NkVendorLocationLocalizedModel>();
            Manufacture = new List<SelectListItem>();
            District = new List<SelectListItem>();
            GenreList = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
        }
        public IList<SelectListItem> GenreList { get; set; }
        public IList<NkVendorLocationLocalizedModel> Locales { get; set; }
        public IList<SelectListItem> Manufacture { get; set; }
        public IList<SelectListItem> District { get; set; }
        public IList<SelectListItem> Categories { get; set; }

    }

    public partial record NkVendorLocationLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }
        [NopResourceDisplayName("Admin.Vendors.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.MapLocation")]
        public string MapLocation { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("Admin.Vendors.Fields.Description")]
        public string Description { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.Fields.SeName")]
        public string SeName { get; set; }
    }
}
