using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.UI.Paging;
using System.Collections.Generic;

namespace Nop.Web.Models.Vendors
{
    public partial record NkVendorLocationPagingFilteringModel : BasePageableModel
    {
        public NkVendorLocationPagingFilteringModel()
        {
            Manufacture = new List<SelectListItem>();
            District = new List<SelectListItem>();
            State = new List<SelectListItem>();
            Genre = new List<SelectListItem>();
        }
        public int ManufacturerId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public int GenreId { get; set; }
        public IList<SelectListItem> Manufacture { get; set; }
        public IList<SelectListItem> District { get; set; }
        public IList<SelectListItem> State { get; set; }
        public IList<SelectListItem> Genre { get; set; }

    }
}
