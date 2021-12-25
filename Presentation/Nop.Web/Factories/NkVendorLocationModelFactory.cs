using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Caching;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Infrastructure.Cache;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Extensions;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Models.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial class NkVendorLocationModelFactory : INkVendorLocationModelFactory
    {
        private readonly INkVendorLocationService _NkVendorLocationService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IManufacturerService _manufacturerService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICountryService _countryService;
        public NkVendorLocationModelFactory(INkVendorLocationService NkVendorLocationService,
            IUrlRecordService urlRecordService,
            IStaticCacheManager staticCacheManager,
            IManufacturerService manufacturerService,
            IStateProvinceService stateProvinceService,
            ICountryService countryService)
        {
            _NkVendorLocationService = NkVendorLocationService;
            _urlRecordService = urlRecordService;
            _staticCacheManager = staticCacheManager;
            _manufacturerService = manufacturerService;
            _stateProvinceService = stateProvinceService;
            _countryService = countryService;
        }
        #region Public Store
        public virtual async Task<NkVendorLocationListModel> PrepareNkVendorLocationSearchModelAsync(NkVendorLocationPagingFilteringModel command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));
            if (command.PageSize <= 0)
                command.PageSize = 10;
            if (command.PageNumber <= 0)
                command.PageNumber = 1;

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.ManufacturersListKey, true);
            var listItems = await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                var manufacturers = await _manufacturerService.GetAllManufacturersAsync(showHidden: true);
                return manufacturers.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
            });

            var result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "*", Value = "0" });
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            command.Manufacture = result;
            var states = await _stateProvinceService.GetStateProvincesByCountryIdAsync((await _countryService.GetCountryByTwoLetterIsoCodeAsync("NP")).Id, showHidden: true);
            command.State.Add(new SelectListItem { Text = "*", Value = "0" });
            foreach (var s in states)
                command.State.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            command.Genre.Add(new SelectListItem { Text = "*", Value = "0" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ServiceCenter), Value = "1" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.SpareParts), Value = "2" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ShowRoom), Value = "3" });

            var vendors = await _NkVendorLocationService.GetAllVendorLocationAsync(showHidden: true,
                districtId: command.DistrictId,
                vendorId: command.ManufacturerId,
                stateId: command.StateId,
                GenreId:command.GenreId,
                pageIndex: command.PageNumber - 1, pageSize: command.PageSize);

            var model = new NkVendorLocationListModel
            {
                PagingFilteringContext = { DistrictId = command.DistrictId,
                    ManufacturerId = command.ManufacturerId,
                    StateId = command.StateId,
                    GenreId=command.GenreId,
                    Manufacture = command.Manufacture,
                    State = command.State,
                    Genre=command.Genre
                },

                VendorLocations = await vendors.SelectAwait(async location =>
                {
                    var vendorLocationModel = new NkVendorLocationModel();
                    await PrepareNkVendorLocationListModelAsync(vendorLocationModel, location);
                    return vendorLocationModel;
                }).ToListAsync()
            };
            //prepare page parameters
            model.PagingFilteringContext.LoadPagedList(vendors);

            return model;
        }
        #endregion
        public virtual async Task PrepareNkVendorLocationListModelAsync(NkVendorLocationModel model, NkVendorLocation vendorLocation)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (vendorLocation == null)
                throw new ArgumentNullException(nameof(vendorLocation));
            var district = await _NkVendorLocationService.GetDistrictByIdAsync(vendorLocation.DistrictId);
            //get vendors
            model.Id = vendorLocation.Id;
            model.Address = vendorLocation.Address;
            model.MapLocation = vendorLocation.Maplocation;
            model.Genre = EnumExtensions.GetDescription((GenreType)vendorLocation.Genre);
            model.ManufacturerName = (await _manufacturerService.GetManufacturerByIdAsync(vendorLocation.ManufacturerId)).Name;
            model.StateName = (await _stateProvinceService.GetStateProvinceByIdAsync(district.ProvinceId)).Name;
            model.DistrictName = district.Name;
            model.ContactPerson = vendorLocation.ContactPerson;
            model.MobileNo = vendorLocation.MobileNo;
            model.DealerName = vendorLocation.DealerName;
            model.PhoneNo = vendorLocation.PhoneNo;
            model.Email = vendorLocation.Email;
            model.MapLocation = vendorLocation.Maplocation;
        }

        public virtual async Task<NkVendorLocationListModel> PrepareVendorLocationSearchModelAsync()
        {

            NkVendorLocationPagingFilteringModel command = new NkVendorLocationPagingFilteringModel();

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.ManufacturersListKey, true);
            var listItems = await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                var manufacturers = await _manufacturerService.GetAllManufacturersAsync(showHidden: true);
                return manufacturers.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
            });

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            result.Add(new SelectListItem { Text = "*", Value = "0" });
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            command.Manufacture = result;
            var states = await _stateProvinceService.GetStateProvincesByCountryIdAsync((await _countryService.GetCountryByTwoLetterIsoCodeAsync("NP")).Id, showHidden: true);
            command.State.Add(new SelectListItem { Text = "*", Value = "0" });
            foreach (var s in states)
                command.State.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            command.Genre.Add(new SelectListItem { Text = "*", Value = "0" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ServiceCenter), Value = "1" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.SpareParts), Value = "2" });
            command.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ShowRoom), Value = "3" });
           
            var model = new NkVendorLocationListModel
            {
                PagingFilteringContext = { DistrictId = command.DistrictId,
                    ManufacturerId = command.ManufacturerId,
                    StateId = command.StateId,
                    GenreId=command.GenreId,
                    Manufacture = command.Manufacture,
                    State = command.State,
                    Genre=command.Genre
                },              
            };
            //prepare page parameters

            return model;
        }


    }

}
