using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
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
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string DealerName { get; set; }
        public IList<SelectListItem> Manufacture { get; set; }
        public IList<SelectListItem> District { get; set; }
        public IList<SelectListItem> State { get; set; }

    }
}
