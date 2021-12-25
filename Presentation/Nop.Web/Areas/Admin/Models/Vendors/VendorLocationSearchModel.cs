using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    public partial record NkVendorLocationSearchModel : BaseSearchModel
    {
        public NkVendorLocationSearchModel()
        {
            Manufacture = new List<SelectListItem>();
            District = new List<SelectListItem>();
            State = new List<SelectListItem>();
        }
        [NopResourceDisplayName("Admin.NkVendorLocation.List.ManufacturerId")]
        public int ManufacturerId { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.List.DistrictId")]
        public int DistrictId { get; set; }
        [NopResourceDisplayName("Admin.NkVendorLocation.List.StateId")]
        public int StateId { get; set; }
        public IList<SelectListItem> Manufacture { get; set; }
        public IList<SelectListItem> District { get; set; }
        public IList<SelectListItem> State { get; set; }

    }
}
