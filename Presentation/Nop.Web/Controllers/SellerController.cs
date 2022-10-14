using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Sellers;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Sellers;
using Nop.Services.Seo;
using Nop.Web.Factories;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Models.Sellers;

namespace Nop.Web.Controllers
{
    public partial class SellerController : BasePublicController
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly ICustomerService _customerService;
        private readonly IDownloadService _downloadService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ISellerAttributeParser _sellerAttributeParser;
        private readonly ISellerAttributeService _sellerAttributeService;
        private readonly ISellerModelFactory _sellerModelFactory;
        private readonly ISellerService _sellerService;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly SellerSettings _sellerSettings;

        #endregion

        #region Ctor

        public SellerController(CaptchaSettings captchaSettings,
            ICustomerService customerService,
            IDownloadService downloadService,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            IUrlRecordService urlRecordService,
            ISellerAttributeParser sellerAttributeParser,
            ISellerAttributeService sellerAttributeService,
            ISellerModelFactory sellerModelFactory,
            ISellerService sellerService,
            IWorkContext workContext,
            IWorkflowMessageService workflowMessageService,
            LocalizationSettings localizationSettings,
            SellerSettings sellerSettings)
        {
            _captchaSettings = captchaSettings;
            _customerService = customerService;
            _downloadService = downloadService;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _sellerAttributeParser = sellerAttributeParser;
            _sellerAttributeService = sellerAttributeService;
            _sellerModelFactory = sellerModelFactory;
            _sellerService = sellerService;
            _workContext = workContext;
            _workflowMessageService = workflowMessageService;
            _localizationSettings = localizationSettings;
            _sellerSettings = sellerSettings;
        }

        #endregion

        #region Utilities

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task UpdatePictureSeoNamesAsync(Vendor seller)
        {
            var picture = await _pictureService.GetPictureByIdAsync(seller.PictureId);
            if (picture != null)
                await _pictureService.SetSeoFilenameAsync(picture.Id, await _pictureService.GetPictureSeNameAsync(seller.Name));
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        protected virtual async Task<string> ParseSellerAttributesAsync(IFormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var attributesXml = "";
            var attributes = await _sellerAttributeService.GetAllSellerAttributesAsync();
            foreach (var attribute in attributes)
            {
                var controlId = $"{NopSellerDefaults.SellerAttributePrefix}{attribute.Id}";
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                            {
                                var selectedAttributeId = int.Parse(ctrlAttributes);
                                if (selectedAttributeId > 0) ;
                                attributesXml = _sellerAttributeParser.AddSellerAttribute(attributesXml,
                                    attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId];
                            if (!StringValues.IsNullOrEmpty(cblAttributes))
                            {
                                foreach (var item in cblAttributes.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                )
                                {
                                    var selectedAttributeId = int.Parse(item);
                                    if (selectedAttributeId > 0) ;
                                    attributesXml = _sellerAttributeParser.AddSellerAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                                }
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = await _sellerAttributeService.GetSellerAttributeValuesAsync(attribute.Id);
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _sellerAttributeParser.AddSellerAttribute(attributesXml,
                                    attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!StringValues.IsNullOrEmpty(ctrlAttributes))
                            {
                                var enteredText = ctrlAttributes.ToString().Trim();
                                attributesXml = _sellerAttributeParser.AddSellerAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.FileUpload:
                    //not supported seller attributes
                    default:
                        break;
                }
            }

            return attributesXml;
        }

        #endregion

        #region Methods

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> ApplySeller()
        {
            //if (!_sellerSettings.AllowCustomersToApplyForSellerAccount)
            //    return RedirectToRoute("Homepage");

            if (!await _customerService.IsRegisteredAsync(await _workContext.GetCurrentCustomerAsync()))
                return Challenge();

            var model = new ApplySellerModel();
            model = await _sellerModelFactory.PrepareApplySellerModelAsync(model, true, false, null);
            return View(model);
        }

        [HttpPost, ActionName("ApplySeller")]
        [AutoValidateAntiforgeryToken]
        [ValidateCaptcha]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> ApplySellerSubmit(ApplySellerModel model, bool captchaValid, IFormFile uploadedFile, IFormCollection form)
        {
            //if (!_sellerSettings.AllowCustomersToApplyForSellerAccount)
            //    return RedirectToRoute("Homepage");

            if (!await _customerService.IsRegisteredAsync(await _workContext.GetCurrentCustomerAsync()))
                return Challenge();

            if (await _customerService.IsAdminAsync(await _workContext.GetCurrentCustomerAsync()))
                ModelState.AddModelError("", await _localizationService.GetResourceAsync("Sellers.ApplyAccount.IsAdmin"));

            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnApplySellerPage && !captchaValid)
            {
                ModelState.AddModelError("", await _localizationService.GetResourceAsync("Common.WrongCaptchaMessage"));
            }

            var pictureId = 0;

            if (uploadedFile != null && !string.IsNullOrEmpty(uploadedFile.FileName))
            {
                try
                {
                    var contentType = uploadedFile.ContentType;
                    var sellerPictureBinary = await _downloadService.GetDownloadBitsAsync(uploadedFile);
                    var picture = await _pictureService.InsertPictureAsync(sellerPictureBinary, contentType, null);

                    if (picture != null)
                        pictureId = picture.Id;
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", await _localizationService.GetResourceAsync("Sellers.ApplyAccount.Picture.ErrorMessage"));
                }
            }

            //seller attributes
            var sellerAttributesXml = await ParseSellerAttributesAsync(form);
            (await _sellerAttributeParser.GetAttributeWarningsAsync(sellerAttributesXml)).ToList()
                .ForEach(warning => ModelState.AddModelError(string.Empty, warning));

            if (ModelState.IsValid)
            {
                var description = Core.Html.HtmlHelper.FormatText(model.Description, false, false, true, false, false, false);
                //disabled by default
                var seller = new Vendor
                {
                    Name = model.Name,
                    Email = model.Email,
                    IsSeller = true,
                    //some default settings
                    PageSize = 6,
                    AllowCustomersToSelectPageSize = true,
                    PageSizeOptions = _sellerSettings.DefaultSellerPageSizeOptions,
                    PictureId = pictureId,
                    Description = description
                };
                await _sellerService.InsertSellerAsync(seller);
                //search engine name (the same as seller name)
                var seName = await _urlRecordService.ValidateSeNameAsync(seller, seller.Name, seller.Name, true);
                await _urlRecordService.SaveSlugAsync(seller, seName, 0);

                //associate to the current customer
                //but a store owner will have to manually add this customer role to "Sellers" role
                //if he wants to grant access to admin area
                (await _workContext.GetCurrentCustomerAsync()).SellerId = seller.Id;
                await _customerService.UpdateCustomerAsync(await _workContext.GetCurrentCustomerAsync());

                //update picture seo file name
                await UpdatePictureSeoNamesAsync(seller);

                //save seller attributes
                await _genericAttributeService.SaveAttributeAsync(seller, NopSellerDefaults.SellerAttributes, sellerAttributesXml);

                //notify store owner here (email)
                await _workflowMessageService.SendNewSellerAccountApplyStoreOwnerNotificationAsync(await _workContext.GetCurrentCustomerAsync(),
                    seller, _localizationSettings.DefaultAdminLanguageId);

                model.DisableFormInput = true;
                model.Result = await _localizationService.GetResourceAsync("Sellers.ApplyAccount.Submitted");
                return View(model);
            }

            //If we got this far, something failed, redisplay form
            model = await _sellerModelFactory.PrepareApplySellerModelAsync(model, false, true, sellerAttributesXml);
            return View(model);
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Info()
        {
            if (!await _customerService.IsRegisteredAsync(await _workContext.GetCurrentCustomerAsync()))
                return Challenge();

            if (await _workContext.GetCurrentSellerAsync() == null || !_sellerSettings.AllowSellersToEditInfo)
                return RedirectToRoute("CustomerInfo");

            var model = new SellerInfoModel();
            model = await _sellerModelFactory.PrepareSellerInfoModelAsync(model, false);
            return View(model);
        }

        [HttpPost, ActionName("Info")]
        [AutoValidateAntiforgeryToken]
        [FormValueRequired("save-info-button")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> Info(SellerInfoModel model, IFormFile uploadedFile, IFormCollection form)
        {
            if (!await _customerService.IsRegisteredAsync(await _workContext.GetCurrentCustomerAsync()))
                return Challenge();

            if (await _workContext.GetCurrentSellerAsync() == null || !_sellerSettings.AllowSellersToEditInfo)
                return RedirectToRoute("CustomerInfo");

            Picture picture = null;

            if (uploadedFile != null && !string.IsNullOrEmpty(uploadedFile.FileName))
            {
                try
                {
                    var contentType = uploadedFile.ContentType;
                    var sellerPictureBinary = await _downloadService.GetDownloadBitsAsync(uploadedFile);
                    picture = await _pictureService.InsertPictureAsync(sellerPictureBinary, contentType, null);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", await _localizationService.GetResourceAsync("Account.SellerInfo.Picture.ErrorMessage"));
                }
            }

            var seller = await _workContext.GetCurrentSellerAsync();
            var prevPicture = await _pictureService.GetPictureByIdAsync(seller.PictureId);

            //seller attributes
            var sellerAttributesXml = await ParseSellerAttributesAsync(form);
            (await _sellerAttributeParser.GetAttributeWarningsAsync(sellerAttributesXml)).ToList()
                .ForEach(warning => ModelState.AddModelError(string.Empty, warning));

            if (ModelState.IsValid)
            {
                var description = Core.Html.HtmlHelper.FormatText(model.Description, false, false, true, false, false, false);

                seller.Name = model.Name;
                seller.Email = model.Email;
                seller.Description = description;

                if (picture != null)
                {
                    seller.PictureId = picture.Id;

                    if (prevPicture != null)
                        await _pictureService.DeletePictureAsync(prevPicture);
                }

                //update picture seo file name
                await UpdatePictureSeoNamesAsync(seller);

                await _sellerService.UpdateSellerAsync(seller);

                //save seller attributes
                await _genericAttributeService.SaveAttributeAsync(seller, NopSellerDefaults.SellerAttributes, sellerAttributesXml);

                //notifications
                if (_sellerSettings.NotifyStoreOwnerAboutSellerInformationChange)
                    await _workflowMessageService.SendSellerInformationChangeNotificationAsync(seller, _localizationSettings.DefaultAdminLanguageId);

                return RedirectToAction("Info");
            }

            //If we got this far, something failed, redisplay form
            model = await _sellerModelFactory.PrepareSellerInfoModelAsync(model, true, sellerAttributesXml);
            return View(model);
        }

        [HttpPost, ActionName("Info")]
        [AutoValidateAntiforgeryToken]
        [FormValueRequired("remove-picture")]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> RemovePicture()
        {
            if (!await _customerService.IsRegisteredAsync(await _workContext.GetCurrentCustomerAsync()))
                return Challenge();

            if (await _workContext.GetCurrentSellerAsync() == null || !_sellerSettings.AllowSellersToEditInfo)
                return RedirectToRoute("CustomerInfo");

            var seller = await _workContext.GetCurrentSellerAsync();
            var picture = await _pictureService.GetPictureByIdAsync(seller.PictureId);

            if (picture != null)
                await _pictureService.DeletePictureAsync(picture);

            seller.PictureId = 0;
            await _sellerService.UpdateSellerAsync(seller);

            //notifications
            if (_sellerSettings.NotifyStoreOwnerAboutSellerInformationChange)
                await _workflowMessageService.SendSellerInformationChangeNotificationAsync(seller, _localizationSettings.DefaultAdminLanguageId);

            return RedirectToAction("Info");
        }

        #endregion
    }
}