using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Sellers;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Sellers
{
    public partial class NkSellerLocationService : INkSellerLocationService
    {

        #region Fields


        private readonly IRepository<Manufacturer> _manufactureRepository;
        private readonly IRepository<NkSellerLocation> _NkSellerLocationRepository;
        private readonly IRepository<StateProvince> _stateProvinceRepository;
        private readonly IRepository<NkDistrict> _districtRepository;
        private readonly IRepository<NkDistrictClone> _districtClonerepository;

        #endregion

        #region Ctor

        public NkSellerLocationService(IRepository<NkSellerLocation> NkSellerLocationRepository,
             IRepository<StateProvince> stateProvinceRepository,
            IRepository<Manufacturer> manufactureRepository,
            IRepository<NkDistrict> districtRepository,
            IRepository<NkDistrictClone> districtCloneRepository
            )
        {
            _NkSellerLocationRepository = NkSellerLocationRepository;
            _stateProvinceRepository = stateProvinceRepository;
            _manufactureRepository = manufactureRepository;
            _districtRepository = districtRepository;
            _districtClonerepository = districtCloneRepository;
        }

        #endregion

        public virtual async Task<IPagedList<NkSellerLocation>> GetNkSellerLocationBySellerAsync(int manufacturerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var query = _NkSellerLocationRepository.Table.Where(vn => vn.ManufacturerId == manufacturerId);

            query = query.OrderBy(v => v.ManufacturerId).ThenBy(v => v.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual async Task<IPagedList<NkSellerLocation>> GetAllSellerLocationAsync(int? manufacturerId, int? districtId, int? stateId, int? genreId, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)

        {


            var query = _NkSellerLocationRepository.Table;
            if (districtId.HasValue && districtId != 0)
                query = query.Where(v => v.DistrictId.Equals(districtId));
            if (manufacturerId.HasValue && manufacturerId != 0)
                query = query.Where(v => v.ManufacturerId.Equals(manufacturerId));
            if (stateId.HasValue && stateId != 0)
                query = (from q in query join d in _districtRepository.Table on q.DistrictId equals d.Id where d.ProvinceId.Equals(stateId) select q);
            if (genreId.HasValue && genreId != 0)
                query = query.Where(s => s.Genre.Equals(genreId));
            query = query.Where(v => !v.Deleted && v.Published);
            return await query.ToPagedListAsync(pageIndex, pageSize);

        }

        public virtual async Task<NkSellerLocation> GetNkSellerLocationByIdAsync(int vendorLocationId)
        {
            if (vendorLocationId == 0)
                return null;

            return await (from v in _manufactureRepository.Table
                          join vd in _NkSellerLocationRepository.Table on v.Id equals vd.ManufacturerId
                          //join s in _stateProvinceRepository.Table
                          where vd.Id == vendorLocationId
                          select vd).FirstOrDefaultAsync();
        }

        public virtual async Task DeleteNkSellerLocationAsync(NkSellerLocation vendor)
        {
            await _NkSellerLocationRepository.DeleteAsync(vendor);
        }

        public virtual async Task InsertNkSellerLocationAsync(NkSellerLocation vendor)
        {
            vendor.CreatedOnUtc = DateTime.Now;
            vendor.UpdatedOnUtc = DateTime.Now;

            await _NkSellerLocationRepository.InsertAsync(vendor);
        }

        public virtual async Task UpdateNkSellerLocationAsync(NkSellerLocation vendor)
        {

            vendor.UpdatedOnUtc = DateTime.Now;
            await _NkSellerLocationRepository.UpdateAsync(vendor);
        }
        public virtual async Task<IPagedList<NkSellerLocation>> GetValidSellerLocationAsync(int? manufacturerId, int? districtId, int? stateId, int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var vendors = await _NkSellerLocationRepository.GetAllPagedAsync(query =>
            {

                if (districtId.HasValue)
                    query = query.Where(v => v.DistrictId.Equals(districtId));

                if (manufacturerId.HasValue)
                    query = query.Where(v => v.ManufacturerId.Equals(manufacturerId));

                if (stateId.HasValue)
                    query = (from q in query join d in _districtRepository.Table on q.DistrictId equals d.Id where d.ProvinceId.Equals(stateId) select q);

                // query = query.Where(v => !v.Deleted && v.Published);
                query = query.OrderBy(v => v.Id).ThenBy(v => v.DistrictId);

                return query;
            }, pageIndex, pageSize);

            return vendors;
        }

        public virtual async Task<IList<Manufacturer>> GetManufacturersAsync()
        {
            return await _manufactureRepository.GetAllAsync(query => { return query; });
        }
        public virtual async Task<IList<StateProvince>> GetStateAsync()
        {
            return await _stateProvinceRepository.GetAllAsync(query => { return query; });

        }
        public virtual async Task<IList<NkDistrict>> GetDistrictByStateAsync(int stateId)
        {
            return await _districtRepository.GetAllAsync(query => { return query.Where(s => s.ProvinceId.Equals(stateId)); });
        }

        public virtual async Task<NkDistrict> GetDistrictByIdAsync(int districtId)
        {
            return await _districtRepository.Table.Where(d => d.Id.Equals(districtId)).FirstOrDefaultAsync();
        }

        public async Task<IPagedList<NkSellerLocation>> GetAllLocationAsync(int? vendorId,
            int? districtId, int? stateId, string address, string dealerName,
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var vendors = await _NkSellerLocationRepository.GetAllPagedAsync(query =>
            {
                if (districtId.HasValue && districtId!=0)
                    query = query.Where(v => v.DistrictId.Equals(districtId));

                if (vendorId.HasValue && vendorId != 0)
                    query = query.Where(v => v.ManufacturerId.Equals(vendorId));

                if (stateId.HasValue && stateId!=0)
                    query = (from q in query join d in _districtRepository.Table on q.DistrictId equals d.Id where d.ProvinceId.Equals(stateId) select q);
                if (!string.IsNullOrEmpty(address))
                    query = query.Where(a => a.Address.Contains(address));
                if (!string.IsNullOrEmpty(dealerName))
                    query = query.Where(a => a.DealerName.Contains(dealerName));

                query = query.OrderBy(v => v.Id).ThenBy(v => v.DistrictId);
                query = query.Where(v => !v.Deleted);
                return query;
            }, pageIndex, pageSize);

            return vendors;
        }
        public virtual async Task<IList<NkDistrict>> GetAllDistrictAsync()
        {
            return await _districtRepository.GetAllAsync(query => { return query; });
        }

        public virtual async Task DeleteSellerLocationAsync(NkSellerLocation vendor)
        {
            await _NkSellerLocationRepository.DeleteAsync(vendor);
        }

    }
}
