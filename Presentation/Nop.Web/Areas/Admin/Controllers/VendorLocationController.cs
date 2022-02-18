using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.ExportImport;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Vendors;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class VendorLocationController : BaseAdminController
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerService _customerService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IVendorAttributeParser _vendorAttributeParser;
        private readonly IVendorAttributeService _vendorAttributeService;
        private readonly INkVendorLocationModelFactory _vendorModelFactory;
        private readonly INkVendorLocationService _vendorService;
        private readonly INkVendorLocationService _NkVendorLocationService;
        private readonly IImportManager _importManager;
        #endregion

        #region Ctor

        public VendorLocationController(IAddressService addressService,
            ICustomerActivityService customerActivityService,
            ICustomerService customerService,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            IUrlRecordService urlRecordService,
            IVendorAttributeParser vendorAttributeParser,
            IVendorAttributeService vendorAttributeService,
            INkVendorLocationModelFactory vendorModelFactory,
            INkVendorLocationService vendorService,
            INkVendorLocationService NkVendorLocationService )
        {
            _addressService = addressService;
            _customerActivityService = customerActivityService;
            _customerService = customerService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _vendorAttributeParser = vendorAttributeParser;
            _vendorAttributeService = vendorAttributeService;
            _vendorModelFactory = vendorModelFactory;
            _vendorService = vendorService;
            _NkVendorLocationService = NkVendorLocationService;
            //_importManager = importManager;
        }

        #endregion

        #region Utilities


        /// <returns>A task that represents the asynchronous operation</returns>



        #endregion

        #region Vendors

        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //prepare model
            var model = await _vendorModelFactory.PrepareNkVendorLocationAdminSearchModelAsync(new NkVendorLocationSearchModel());
            return View(model);
        }

        [HttpPost]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> List(NkVendorLocationSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _vendorModelFactory.PrepareNkVendorLocationListModelAsync(searchModel);

            return Json(model);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //prepare model
            var model = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(new CreateVendorLocationModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Create(CreateVendorLocationModel vendorLocation, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //parse vendor attributes


            if (ModelState.IsValid)
            {
                var vendor = new NkVendorLocation
                {
                    Id = vendorLocation.Id,
                    Address = vendorLocation.Address,
                    Maplocation = vendorLocation.Maplocation,
                    Genre = vendorLocation.Genre,
                    ContactPerson = vendorLocation.ContactPerson,
                    MobileNo = vendorLocation.MobileNo,
                    DealerName = vendorLocation.DealerName,
                    PhoneNo = vendorLocation.PhoneNo,
                    Email = vendorLocation.Email,
                    CategoryId = vendorLocation.CategoryId,
                    DistrictId = vendorLocation.DistrictId,
                    ManufacturerId = vendorLocation.ManufacturerId,
                    Published = true,
                    Deleted = false

                };

                await _vendorService.InsertNkVendorLocationAsync(vendor);

                //activity log
                await _customerActivityService.InsertActivityAsync("AddNewVendor",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewVendor"), vendor.Id), vendor);

                //search engine name


                //some validation

                await _vendorService.UpdateNkVendorLocationAsync(vendor);




                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.NkVendorLocation.Added"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = vendor.Id });
            }

            //prepare model
            vendorLocation = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(vendorLocation, null, true);

            //if we got this far, something failed, redisplay form
            return View(vendorLocation);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            var vendor = await _vendorService.GetNkVendorLocationByIdAsync(id);
            if (vendor == null || vendor.Deleted)
                return RedirectToAction("List");

            //prepare model
            var model = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(null, vendor);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Edit(CreateVendorLocationModel vendorLocation, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            var vendor = await _vendorService.GetNkVendorLocationByIdAsync(vendorLocation.Id);
            if (vendor == null || vendor.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {

                vendor = new NkVendorLocation
                {
                    Id = vendorLocation.Id,
                    Address = vendorLocation.Address,
                    Maplocation = vendorLocation.Maplocation,
                    Genre = vendorLocation.Genre,
                    ContactPerson = vendorLocation.ContactPerson,
                    MobileNo = vendorLocation.MobileNo,
                    DealerName = vendorLocation.DealerName,
                    PhoneNo = vendorLocation.PhoneNo,
                    Email = vendorLocation.Email,
                    CategoryId = vendorLocation.CategoryId,
                    DistrictId = vendorLocation.DistrictId,
                    ManufacturerId = vendorLocation.ManufacturerId,
                    Published = true,
                    Deleted = false,
                    UpdatedOnUtc=DateTime.Now

                };
                await _vendorService.UpdateNkVendorLocationAsync(vendor);


                //activity log
                await _customerActivityService.InsertActivityAsync("EditVendorLocation",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditLocationVendor"), vendor.Id), vendor);

                //search engine name


                //locales

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.VendorsLocation.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = vendor.Id });
            }

            //prepare model
            vendorLocation = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(vendorLocation, vendor, true);

            //if we got this far, something failed, redisplay form
            return View(vendorLocation);
        }
        [HttpPost]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            var vendor = await _vendorService.GetNkVendorLocationByIdAsync(id);
            if (vendor == null)
                return RedirectToAction("List");



            //delete a vendor
            await _vendorService.DeleteVendorLocationAsync(vendor);

            //activity log
            await _customerActivityService.InsertActivityAsync("DeleteVendor",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeleteVendor"), vendor.Id), vendor);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Vendors.Deleted"));

            return RedirectToAction("List");
        }

        #endregion
        //[HttpPost]
        ///// <returns>A task that represents the asynchronous operation</returns>
        //public virtual async Task<IActionResult> ImportFromXlsx(IFormFile importexcelfile)
        //{
        //    if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
        //        return AccessDeniedView();

        //    //a vendor cannot import categories
          

        //    try
        //    {
        //        if (importexcelfile != null && importexcelfile.Length > 0)
        //        {
        //            //await _importManager.ImportVendorLocationFromXlsxAsync(importexcelfile.OpenReadStream());
        //        }
        //        else
        //        {
        //            _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("Admin.Common.UploadFile"));
        //            return RedirectToAction("List");
        //        }

        //        _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.VendorLocation.Imported"));

        //        return RedirectToAction("List");
        //    }
        //    catch (Exception exc)
        //    {
        //        await _notificationService.ErrorNotificationAsync(exc);
        //        return RedirectToAction("List");
        //    }
        //}


    }
}
