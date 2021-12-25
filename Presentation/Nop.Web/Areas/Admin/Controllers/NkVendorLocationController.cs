using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Customers;
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
    public partial class NkVendorLocationController : BaseAdminController
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
        #endregion

        #region Ctor

        public NkVendorLocationController(IAddressService addressService,
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
            INkVendorLocationService NkVendorLocationService)
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
            var model = await _vendorModelFactory.PrepareNkVendorLocationSearchModelAsync(new NkVendorLocationSearchModel());
            return View(model);
        }

        [HttpPost]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> VendorLocationList(NkVendorLocationSearchModel searchModel)
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
            var model = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(new NkVendorLocationModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Create(NkVendorLocationModel model, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //parse vendor attributes


            if (ModelState.IsValid)
            {
                var vendor = model.ToEntity<NkVendorLocation>();
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
            model = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(model, null, true);

            //if we got this far, something failed, redisplay form
            return View(model);
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
        public virtual async Task<IActionResult> Edit(NkVendorLocationModel model, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageVendors))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            var vendor = await _vendorService.GetNkVendorLocationByIdAsync(model.Id);
            if (vendor == null || vendor.Deleted)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {

                vendor = model.ToEntity(vendor);
                await _vendorService.UpdateNkVendorLocationAsync(vendor);


                //activity log
                await _customerActivityService.InsertActivityAsync("EditVendor",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditVendor"), vendor.Id), vendor);

                //search engine name
               

                //locales
          
                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Vendors.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = vendor.Id });
            }

            //prepare model
            model = await _vendorModelFactory.PrepareNkVendorLocationModelAsync(model, vendor, true);

            //if we got this far, something failed, redisplay form
            return View(model);
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

            //clear associated customer references
            var associatedCustomers = await _customerService.GetAllCustomersAsync(vendorId: vendor.Id);
            foreach (var customer in associatedCustomers)
            {
                customer.VendorId = 0;
                await _customerService.UpdateCustomerAsync(customer);
            }

            //delete a vendor
            await _vendorService.DeleteNkVendorLocationAsync(vendor);

            //activity log
            await _customerActivityService.InsertActivityAsync("DeleteVendor",
                string.Format(await _localizationService.GetResourceAsync("ActivityLog.DeleteVendor"), vendor.Id), vendor);

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Vendors.Deleted"));

            return RedirectToAction("List");
        }

        public virtual async Task<IActionResult> GetDistrictByStateId(string stateId, bool? addSelectStateItem, bool? addAsterisk)
        {
            //permission validation is not required here

            // This action method gets called via an ajax request
            if (string.IsNullOrEmpty(stateId))
                throw new ArgumentNullException(nameof(stateId));

            var district = await _NkVendorLocationService.GetDistrictByStateAsync(Convert.ToInt32(stateId));
            var result = (from s in district
                          select new { id = s.Id, name = s.Name }).ToList();
            if (addAsterisk.HasValue && addAsterisk.Value)
            {
                //asterisk
                result.Insert(0, new { id = 0, name = "--Select--" });
            }
            return Json(result);
        }
        #endregion


    }
}
