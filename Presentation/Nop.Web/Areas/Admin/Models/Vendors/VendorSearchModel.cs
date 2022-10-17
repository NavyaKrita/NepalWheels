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
       
        #region Properties

        [NopResourceDisplayName("Admin.Vendors.List.SearchName")]
        public string SearchName { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.SearchEmail")]
        public string SearchEmail { get; set; }

        [NopResourceDisplayName("Admin.Vendors.List.IsSeller")]
        public bool SearchSeller { get; set; }
        [NopResourceDisplayName("Admin.Vendors.List.CustomerType")]
        public int SearchGroupId { get; set; }
        public IList<SelectListItem> CustomerTypes { get; set; }
        #endregion
    }
}