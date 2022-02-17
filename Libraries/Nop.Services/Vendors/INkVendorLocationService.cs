using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Vendors
{
    public partial interface INkVendorLocationService
    {
        Task<IPagedList<NkVendorLocation>> GetNkVendorLocationByVendorAsync(int vendorId, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IPagedList<NkVendorLocation>> GetAllVendorLocationAsync(int? vendorId, int? districtId, int? stateId,int? GenreId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
       

        Task<NkVendorLocation> GetNkVendorLocationByIdAsync(int NkVendorLocationId);

        Task DeleteNkVendorLocationAsync(NkVendorLocation vendor);
        Task InsertNkVendorLocationAsync(NkVendorLocation vendor);
        Task UpdateNkVendorLocationAsync(NkVendorLocation vendor);
        Task<IPagedList<NkVendorLocation>> GetValidVendorLocationAsync(int? manufacturerId, int? districtId, int? stateId, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IList<Manufacturer>> GetManufacturersAsync();
        Task<IList<StateProvince>> GetStateAsync();
        Task<IList<NkDistrict>> GetDistrictByStateAsync(int stateId);
        Task<NkDistrict> GetDistrictByIdAsync(int districtId);
        Task<IList<NkDistrict>> GetAllDistrictAsync();

        Task<IPagedList<NkVendorLocation>> GetAllLocationAsync(int? vendorId,
           int? districtId, int? stateId, string address, string dealerName,
           int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task DeleteVendorLocationAsync(NkVendorLocation vendor);
    }
}
