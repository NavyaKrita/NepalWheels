using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Sellers
{
    public partial record NkSellerLocationListModel : BaseNopModel
    {
        public NkSellerLocationListModel()
        {
            PagingFilteringContext = new NkSellerLocationPagingFilteringModel();
            VendorLocations = new List<NkSellerLocationModel>();
        
        }
              
        public NkSellerLocationPagingFilteringModel PagingFilteringContext { get; set; }
        public IList<NkSellerLocationModel> VendorLocations { get; set; }
        
    }
}
