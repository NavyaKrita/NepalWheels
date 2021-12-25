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
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial class NkVendorLocationModelFactory : INkVendorLocationModelFactory
    {
        
        private readonly INkVendorLocationService _NkVendorLocationService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IManufacturerService _manufacturerService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        public NkVendorLocationModelFactory(INkVendorLocationService NkVendorLocationService,
            IUrlRecordService urlRecordService,
            IStaticCacheManager staticCacheManager,
            IManufacturerService manufacturerService,
            IStateProvinceService stateProvinceService,
            ICountryService countryService,
            ILocalizationService localizationService)
        {
            _NkVendorLocationService = NkVendorLocationService;
            _urlRecordService = urlRecordService;
            _staticCacheManager = staticCacheManager;
            _manufacturerService = manufacturerService;
            _stateProvinceService = stateProvinceService;
            _countryService = countryService;
            _localizationService = localizationService;
        }

        public virtual  Task<NkVendorLocationSearchModel> PrepareNkVendorLocationSearchModelAsync(NkVendorLocationSearchModel searchModel)
        {
            //if (searchModel == null)
            //    throw new ArgumentNullException(nameof(searchModel));


            //var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.ManufacturersListKey, true);
            //var listItems = await _staticCacheManager.GetAsync(cacheKey, async () =>
            //{
            //    var manufacturers = await _manufacturerService.GetAllManufacturersAsync(showHidden: true);
            //    return manufacturers.Select(m => new SelectListItem
            //    {
            //        Text = m.Name,
            //        Value = m.Id.ToString()
            //    });
            //});

            //var result = new List<SelectListItem>();
            ////clone the list to ensure that "selected" property is not set
            //foreach (var item in listItems)
            //{
            //    result.Add(new SelectListItem
            //    {
            //        Text = item.Text,
            //        Value = item.Value
            //    });
            //}

            //searchModel.Manufacture = result;
            //var states = await _stateProvinceService.GetStateProvincesByCountryIdAsync((await _countryService.GetCountryByTwoLetterIsoCodeAsync("NP")).Id, showHidden: true);
            //searchModel.State.Add(new SelectListItem { Text = "*", Value = "0" });
            //foreach (var s in states)
            //    searchModel.State.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            ////prepare page parameters
            //searchModel.SetGridPageSize();

            //return searchModel;
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }
        public virtual async Task<NkVendorLocationListModel> PrepareNkVendorLocationListModelAsync(NkVendorLocationSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get vendors
            var vendors = await _NkVendorLocationService.GetAllLocationAsync(showHidden: true,
                districtId: searchModel.DistrictId,
                vendorId: searchModel.ManufacturerId,
                stateId: searchModel.StateId,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = await new NkVendorLocationListModel().PrepareToGridAsync(searchModel, vendors, () =>
            {
                //fill in model values from the entity
                return vendors.SelectAwait(async vendor =>
                {
                    var nkVendorLocation = vendor.ToModel<NkVendorLocationModel>();

                    //nkVendorLocation.SeName = await _urlRecordService.GetSeNameAsync(vendor, 0, true, false);

                    return nkVendorLocation;
                });
            });
           
            return model;
        }
        public virtual async Task<NkVendorLocationModel> PrepareNkVendorLocationModelAsync(NkVendorLocationModel model, NkVendorLocation vendor, bool excludeProperties = false)
        {
           
            if (vendor != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = vendor.ToModel<NkVendorLocationModel>();
                   
                }

                //define localized model configuration action
               

            }

            //set default values for the new model
            if (vendor == null)
            {
                model.ManufacturerId = 6;
                model.DistrictId = 1;
                model.Genre = 1;
                model.Deleted = true;
                model.Published = true;
                model.MapLocation = "";
                model.Address = "";
                model.DistrictName = "Kathmandu";
                model.ManufacturerName = "NavyaKritta";
            }
            return model;
        }
    }
}
