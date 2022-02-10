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
using Nop.Web.Extensions;
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
        private readonly ICategoryService _categoryService;
        public NkVendorLocationModelFactory(INkVendorLocationService NkVendorLocationService,
            IUrlRecordService urlRecordService,
            IStaticCacheManager staticCacheManager,
            IManufacturerService manufacturerService,
            IStateProvinceService stateProvinceService,
            ICountryService countryService,
            ILocalizationService localizationService,
             ICategoryService categoryService)
        {
            _NkVendorLocationService = NkVendorLocationService;
            _urlRecordService = urlRecordService;
            _staticCacheManager = staticCacheManager;
            _manufacturerService = manufacturerService;
            _stateProvinceService = stateProvinceService;
            _countryService = countryService;
            _localizationService = localizationService;
            _categoryService = categoryService;
        }

        public virtual async Task<NkVendorLocationSearchModel> PrepareNkVendorLocationSearchModelAsync(NkVendorLocationSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));


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
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            searchModel.Manufacture = result;
            var states = await _stateProvinceService.GetStateProvincesByCountryIdAsync((await _countryService.GetCountryByTwoLetterIsoCodeAsync("NP")).Id, showHidden: true);
            searchModel.State.Add(new SelectListItem { Text = "*", Value = "0" });
            foreach (var s in states)
                searchModel.State.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
        }


        public virtual async Task<NkVendorLocationSearchModel> PrepareNkVendorLocationAdminSearchModelAsync(NkVendorLocationSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));


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
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }


            var district = await _NkVendorLocationService.GetAllDistrictAsync();

            var districtresult = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in district)
            {
                districtresult.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            result.Add(new SelectListItem { Text = "*", Value = "0" });
            searchModel.Manufacture = result;
            districtresult.Add(new SelectListItem { Text = "*", Value = "0" });

            searchModel.District = districtresult;
            //searchModel.Genre.Add(new SelectListItem { Text = "*", Value = "0" });
            //searchModel.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ServiceCenter), Value = "1" });
            //searchModel.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.SpareParts), Value = "2" });
            //searchModel.Genre.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ShowRoom), Value = "3" });

            //prepare page parameters
            searchModel.SetGridPageSize();

            return searchModel;
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
                address: searchModel.Address,
                dealerName: searchModel.DealerName,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            try
            {
                var model = await new NkVendorLocationListModel().PrepareToGridAsync(searchModel, vendors, () =>
                {
                    //fill in model values from the entity
                    return vendors.SelectAwait(async vendorLocation =>
                    {
                        var vendorModel = new NkVendorLocationModel();
                        vendorModel.ManufacturerName = (await _manufacturerService.GetManufacturerByIdAsync(vendorLocation.ManufacturerId)).Name;
                        vendorModel.DistrictName = (await _NkVendorLocationService.GetDistrictByIdAsync(vendorLocation.DistrictId)).Name;
                        vendorModel.Id = vendorLocation.Id;
                        vendorModel.Address = vendorLocation.Address;
                        vendorModel.Maplocation = vendorLocation.Maplocation;
                        vendorModel.Genre = vendorLocation.Genre;
                        vendorModel.ContactPerson = vendorLocation.ContactPerson;
                        vendorModel.MobileNo = vendorLocation.MobileNo;
                        vendorModel.DealerName = vendorLocation.DealerName;
                        vendorModel.PhoneNo = vendorLocation.PhoneNo;
                        vendorModel.Email = vendorLocation.Email;
                        vendorModel.GenreName = EnumExtensions.GetDescription((GenreType)vendorLocation.Genre);

                        return vendorModel;
                    });
                });
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
            //prepare list model



        }

        public virtual async Task<CreateVendorLocationModel> PrepareNkVendorLocationModelAsync(CreateVendorLocationModel model, NkVendorLocation vendorLocation, bool excludeProperties = false)
        {

            if (vendorLocation != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    var vendorModel = new NkVendorLocationModel();
                   
                    vendorModel.Id = vendorLocation.Id;
                    vendorModel.Address = vendorLocation.Address;
                    vendorModel.Maplocation = vendorLocation.Maplocation;
                    vendorModel.Genre = vendorLocation.Genre;
                    vendorModel.ContactPerson = vendorLocation.ContactPerson;
                    vendorModel.MobileNo = vendorLocation.MobileNo;
                    vendorModel.DealerName = vendorLocation.DealerName;
                    vendorModel.PhoneNo = vendorLocation.PhoneNo;
                    vendorModel.Email = vendorLocation.Email;                  
                    vendorModel.CategoryId = vendorLocation.CategoryId;
                    vendorModel.DistrictId = vendorLocation.DistrictId;
                    vendorModel.ManufacturerId = vendorLocation.ManufacturerId;

                    model.Locales = new List<NkVendorLocationLocalizedModel>();
                    model.Manufacture = new List<SelectListItem>();
                    model.District = new List<SelectListItem>();
                    model. GenreList = new List<SelectListItem>();
                    model.Categories = new List<SelectListItem>();
                }
            }

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
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            model.Manufacture = result;

            //define localized model configuration action

            var district = await _NkVendorLocationService.GetAllDistrictAsync();

            var districtresult = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in district)
            {
                districtresult.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }

            model.District = districtresult;
            model.GenreList.Add(new SelectListItem { Text = "*", Value = "0" });
            model.GenreList.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ServiceCenter), Value = "1" });
            model.GenreList.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.SpareParts), Value = "2" });
            model.GenreList.Add(new SelectListItem { Text = EnumExtensions.GetDescription(GenreType.ShowRoom), Value = "3" });

            var categories = await _categoryService.GetAllCategoriesAsync(showHidden: true);
            var catcacheKey = _staticCacheManager.PrepareKeyForDefaultCache(NopModelCacheDefaults.CategoriesListKey, true);
            var catlistItems = await _staticCacheManager.GetAsync(catcacheKey, async () =>
            {
                var categories = await _categoryService.GetAllCategoriesAsync(showHidden: true);
                return categories.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });
            });

            var catresult = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in catlistItems)
            {
                catresult.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }
            model.Categories = catresult;
            return model;
        }
    }
}
