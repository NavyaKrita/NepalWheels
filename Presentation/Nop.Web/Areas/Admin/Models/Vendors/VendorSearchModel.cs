using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Nop.Web.Areas.Admin.Models.Vendors
{
    /// <summary>
    /// Represents a vendor search model
    /// </summary>
    public partial record VendorSearchModel : BaseSearchModel
    {
        public VendorSearchModel()
        {
        Type = new List<SelectListItem>() { 
        new SelectListItem { Text = "Vendor", Value ="1"},
        new SelectListItem { Text = "Seller", Value = "2"},
        };
        }

        #region Properties

        [NopResourceDisplayName("Admin.Vendors.List.SearchName")]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.SearchEmail")]
        public string SearchEmail { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.SearchType")]
        public string SearchType { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public IList<SelectListItem> Type { get; set; }
        #endregion
    }
}