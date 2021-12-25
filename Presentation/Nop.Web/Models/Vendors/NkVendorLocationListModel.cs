using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Vendors
{
    public partial record NkVendorLocationListModel : BaseNopModel
    {
        public NkVendorLocationListModel()
        {
            PagingFilteringContext = new NkVendorLocationPagingFilteringModel();
            VendorLocations = new List<NkVendorLocationModel>();
        
        }
              
        public NkVendorLocationPagingFilteringModel PagingFilteringContext { get; set; }
        public IList<NkVendorLocationModel> VendorLocations { get; set; }
        
    }
}
