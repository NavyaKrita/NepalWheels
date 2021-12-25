using Nop.Web.Models.Vendors;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial interface INkVendorLocationModelFactory
    {
        #region NkVendorLocations
        Task<NkVendorLocationListModel> PrepareNkVendorLocationSearchModelAsync(NkVendorLocationPagingFilteringModel command);
        Task<NkVendorLocationListModel> PrepareVendorLocationSearchModelAsync();       
        #endregion
    }
}
