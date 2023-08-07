using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Notice;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Messages;
using Nop.Services.Notice;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.Notice;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.Notice;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class NoticeBoardController : BaseAdminController
    {
        private readonly IPermissionService _permissionService;
        private readonly INoticeBoardModelFactory _noticeBoardModelFactory;
        private readonly INoticeBoardService _noticeBoardService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;

        public NoticeBoardController(IPermissionService permissionService,
           INoticeBoardModelFactory noticeBoardModelFactory,
          INoticeBoardService noticeBoardService,
          ICustomerActivityService customerActivityService,
           ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService)
        {
            _permissionService = permissionService;
            _noticeBoardModelFactory = noticeBoardModelFactory;
            _noticeBoardService = noticeBoardService;
            _customerActivityService = customerActivityService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _notificationService = notificationService;

        }
        // GET: NoticeBoardController
        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> List()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareNoticeBoardSearchModelAsync(new NoticeBoardSearchModel());

            return View(model);
        }

        [HttpPost]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> List(NoticeBoardSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareNoticeBoardListModelAsync(searchModel);

            return Json(model);
        }

        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareNoticeBoardModelAsync(new CreateNoticeBoardModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Create(CreateNoticeBoardModel noticeBoard, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //parse  attributes

            NoticeBoard model = new();
            if (ModelState.IsValid)
            {
                model.Id = noticeBoard.Id;
                model.Title = noticeBoard.Title;
                model.Notice = noticeBoard.Notice;
                model.DisplayForm = noticeBoard.DisplayForm;
                model.PublishedFrom = noticeBoard.PublishedFrom;
                model.PublishedTo = noticeBoard.PublishedTo;
                model.CreatedOnUtc = System.DateTime.Today;
                model.ThankYou = noticeBoard.ThankYou;
                model.TermsAndCondition = noticeBoard.TermsAndCondition;
                model.Name = noticeBoard.Name;
                model.PhoneNumber = noticeBoard.PhoneNumber;
                model.EmailAddress = noticeBoard.EmailAddress;
                model.Age = noticeBoard.Age;
                model.Address = noticeBoard.Address;
                model.City = noticeBoard.City;
                model.BikeName = noticeBoard.BikeName;
                model.CC = noticeBoard.CC;
                model.ManufacturerId = noticeBoard.ManufacturerId;
                model.URL = noticeBoard.URL;
                model.Products = string.Join(",", noticeBoard.SelectedProductIds);
                model.ButtonDisplayText = noticeBoard.ButtonDisplayText;
                model.InURL = noticeBoard.InURL;
                await _noticeBoardService.InsertNoticeAsync(model);

                //activity log
                await _customerActivityService.InsertActivityAsync("AddNewNoticeBoard",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.AddNewNoticeBoard"), model.Id), model);

                //search engine name          

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Notice.Added"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = model.Id });

            };
            //prepare model
            noticeBoard = await _noticeBoardModelFactory.PrepareNoticeBoardModelAsync(noticeBoard, null, true);

            //if we got this far, something failed, redisplay form
            return View(noticeBoard);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            var noticeBoard = await _noticeBoardService.GetNoticeByIdAsync(id);
            if (noticeBoard == null)
                return RedirectToAction("List");

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareNoticeBoardModelAsync(null, noticeBoard);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Edit(CreateNoticeBoardModel noticeBoard, bool continueEditing, IFormCollection form)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //try to get a vendor with the specified id
            NoticeBoard model = await _noticeBoardService.GetNoticeByIdAsync(noticeBoard.Id);
            if (model == null)
                return RedirectToAction("List");
            model = new();
            if (ModelState.IsValid)
            {

                model.Id = noticeBoard.Id;
                model.Title = noticeBoard.Title;
                model.Notice = noticeBoard.Notice;
                model.DisplayForm = noticeBoard.DisplayForm;
                model.PublishedFrom = noticeBoard.PublishedFrom;
                model.PublishedTo = noticeBoard.PublishedTo;
                model.ThankYou = noticeBoard.ThankYou;
                model.TermsAndCondition = noticeBoard.TermsAndCondition;
                model.Name = noticeBoard.Name;
                model.PhoneNumber = noticeBoard.PhoneNumber;
                model.EmailAddress = noticeBoard.EmailAddress;
                model.Age = noticeBoard.Age;
                model.Address = noticeBoard.Address;
                model.City = noticeBoard.City;
                model.BikeName = noticeBoard.BikeName;
                model.CC = noticeBoard.CC;
                model.ManufacturerId = noticeBoard.ManufacturerId;
                model.URL = noticeBoard.URL;
                model.Products = string.Join(",", noticeBoard.SelectedProductIds);
                model.ButtonDisplayText = noticeBoard.ButtonDisplayText;
                model.CreatedOnUtc = System.DateTime.Today;
                model.InURL = noticeBoard.InURL;
                await _noticeBoardService.UpdateNoticeAsync(model);


                //activity log
                await _customerActivityService.InsertActivityAsync("EditNotice",
                    string.Format(await _localizationService.GetResourceAsync("ActivityLog.EditNotice"), model.Id), model);

                //search engine name


                //locales

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Notice.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = model.Id });
            }

            //prepare model
            noticeBoard = await _noticeBoardModelFactory.PrepareNoticeBoardModelAsync(noticeBoard, model, true);

            //if we got this far, something failed, redisplay form
            return View(noticeBoard);
        }

        public virtual async Task<IActionResult> Participants()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return AccessDeniedView();

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareParticipantsSearchModelAsync(new NoticeBoardDetailSearchModel());

            return View(model);
        }

        [HttpPost]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Participants(NoticeBoardDetailSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _noticeBoardModelFactory.PrepareParticipantsListModelAsync(searchModel);

            return Json(model);
        }

        [HttpGet]
        public virtual async Task<IActionResult> RegisterParticipants()
        {

            string browserUrl = HttpContext.Request.Headers["Referer"].ToString();
            var url = browserUrl.Split("/");

            var model = await _noticeBoardModelFactory.PrepareNoticeModelAsync(browserUrl);
            model.Module = url[4];
            return View(model);
        }

        [HttpGet]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> ManufacturerFeaturedProductsAsync(int manufacturerId)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return await AccessDeniedDataTablesJson();

            //prepare model
            var model = await _noticeBoardModelFactory.GetManufacturerFeaturedProductsAsync(manufacturerId);

            return Json(model);
        }
    }
}
