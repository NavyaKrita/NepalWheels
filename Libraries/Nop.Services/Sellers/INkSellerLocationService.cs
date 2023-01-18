using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Sellers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nop.Services.Sellers
{
    public partial interface INkSellerLocationService
    {
        Task<IPagedList<NkSellerLocation>> GetNkSellerLocationBySellerAsync(int vendorId, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IPagedList<NkSellerLocation>> GetAllSellerLocationAsync(int? vendorId, int? districtId, int? stateId,int? GenreId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
       

        Task<NkSellerLocation> GetNkSellerLocationByIdAsync(int NkSellerLocationId);

        Task DeleteNkSellerLocationAsync(NkSellerLocation vendor);
        Task InsertNkSellerLocationAsync(NkSellerLocation vendor);
        Task UpdateNkSellerLocationAsync(NkSellerLocation vendor);
        Task<IPagedList<NkSellerLocation>> GetValidSellerLocationAsync(int? manufacturerId, int? districtId, int? stateId, int pageIndex = 0, int pageSize = int.MaxValue);
        Task<IList<Manufacturer>> GetManufacturersAsync();
        Task<IList<StateProvince>> GetStateAsync();
        Task<IList<NkDistrict>> GetDistrictByStateAsync(int stateId);
        Task<NkDistrict> GetDistrictByIdAsync(int districtId);
        Task<IList<NkDistrict>> GetAllDistrictAsync();

        Task<IPagedList<NkSellerLocation>> GetAllLocationAsync(int? vendorId,
           int? districtId, int? stateId, string address, string dealerName,
           int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        Task DeleteSellerLocationAsync(NkSellerLocation seller);
    }
}
