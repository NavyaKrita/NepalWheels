﻿using LinqToDB.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Notice;
using Nop.Services.Blogs;
using Nop.Services.Notice;
using Nop.Services.Seo;
using Nop.Web.Factories;
using Nop.Web.Models.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{

    public partial class ParticipantsController : BasePublicController
    {
        private readonly INoticeBoardService _noticeBoardService;
        private readonly INoticeBoardModelFactory _noticeBoardModelFactory;
        private readonly IWebHelper _webHelper;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IBlogService _blogService;
        public ParticipantsController(INoticeBoardService noticeBoardService,
           INoticeBoardModelFactory noticeBoardModelFactory,
           IWebHelper webHelper,
        IUrlRecordService urlRecordService,
        IBlogService blogService)
        {
            _noticeBoardService = noticeBoardService;
            _noticeBoardModelFactory = noticeBoardModelFactory;
            _webHelper = webHelper;
            _urlRecordService = urlRecordService;
            _blogService = blogService;
        }
        public virtual async Task<IActionResult> RegisterParticipants(int id)
        {
            var notice = await _noticeBoardModelFactory.PrepareNoticeModelAsync(id);

            ParticipantsModel model = new()
            {
                NoticeBoardId = notice.Id,
                Notice = notice.Notice,
                Title = notice.Title,

                TermAndConditions = notice.TermAndConditions,
                ThankYou = notice.ThankYou,
                ButtonDisplayText = notice.ButtonDisplayText,
                URL = notice.RedirectionURL,
                InURL = notice.InURL,
                DisplayPopUpInSamePage = notice.DisplayPopUpInSamePage,
                FormPopUpType = notice.FormPopUpType,
                RedirectionURL = notice.RedirectionURL,
                Timer = notice.Timer,
                Products = notice.Products,
                ParticipantField = new()
                {
                    Name = notice.NoticeField.Name,
                    PhoneNumber = notice.NoticeField.PhoneNumber,
                    EmailAddress = notice.NoticeField.EmailAddress,
                    Age = notice.NoticeField.Age,
                    Address = notice.NoticeField.Address,
                    City = notice.NoticeField.City,
                    BikeName = notice.NoticeField.BikeName,
                    CC = notice.NoticeField.CC
                }
            };
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> RegisterParticipants(ParticipantsModel model, IFormCollection form)
        {
            var latestNotice = await _noticeBoardService.GetNoticeByIdAsync(model.NoticeBoardId);

            if (latestNotice.PhoneNumber && string.IsNullOrEmpty(model.PhoneNumber))
            {
                return Json(new { success = false });
            }
            else if (latestNotice.Name && string.IsNullOrEmpty(model.Name))
            {
                return Json(new { success = false });
            }
            else if (latestNotice.EmailAddress && string.IsNullOrEmpty(model.EmailAddress))
            {
                return Json(new { success = false });
            }
            else if (latestNotice.Age && string.IsNullOrEmpty(model.Age))
            {
                return Json(new { success = false });
            }
            else if (latestNotice.Address && string.IsNullOrEmpty(model.Address))
            {
                return Json(new { success = false });
            }
            else if (latestNotice.City && string.IsNullOrEmpty(model.City))
            {
                return Json(new { success = false });
            }


            if (ModelState.IsValid)
            {
                NoticeBoardDetail detail = new()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                    BikeName = model.BikeName,
                    Address = model.Address,
                    Age = model.Age is null ? 0 : !model.Age.All(char.IsDigit) ? 0 : Convert.ToInt32(model.Age),
                    CC = model.CC,
                    ManufacturerId = model.ManufacturerId,
                    Products = model.ProductId,
                };


                detail.Notice = latestNotice?.Title;
                detail.NoticeBoardId = latestNotice?.Id;
                await _noticeBoardService.InsertNoticeParticipatesAsync(detail);

                return Json(new { success = true, displayThankYou = !string.IsNullOrEmpty(latestNotice.ThankYou) });
            }

            //If we got this far, something failed, redisplay form


            return Json(new { success = false });
        }

        [HttpPost]

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> RegisterParticipant(ParticipantModel model, IFormCollection form)
        {
            string browserUrl = HttpContext.Request.Headers["Referer"].ToString();
            var url = browserUrl.Split("/");
            int count = url.Count();

            try
            {
                if (form.ContainsKey("Name") && string.IsNullOrEmpty(model.Name))
                {

                    return Json(new { success = false, message = "Name is required." });

                }
                if (form.ContainsKey("EmailAddress") && string.IsNullOrEmpty(model.EmailAddress))
                {
                    return Json(new { success = false, message = "EmailAddress is required." });
                }
                if (form.ContainsKey("PhoneNumber") && string.IsNullOrEmpty(model.PhoneNumber))
                {

                    return Json(new { success = false, message = "PhoneNumber is required." });
                }
                if (form.ContainsKey("ProductId") && string.IsNullOrEmpty(model.ProductId))
                {
                    return Json(new { success = false, message = "product is required." });
                }

                //if (!string.IsNullOrEmpty(model.PhoneNumber) && model.PhoneNumber.All(char.IsDigit))
                //    return Json(new { success = false, message = "Invalid phone." });

                NoticeBoardDetail detail = new()
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    EmailAddress = model.EmailAddress,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                    BikeName = model.BikeName,
                    Address = model.Address,
                    Age = model.Age is null ? 0 : !model.Age.All(char.IsDigit) ? 0 : Convert.ToInt32(model.Age),
                    CC = model.CC,
                    ManufacturerId = model.ManufacturerId,
                    Products = model.ProductId,

                };
                if (count > 0)
                    detail.LeadGenerate = url[count - 1];
                await _noticeBoardService.InsertNoticeParticipatesAsync(detail);

                return Json(new { success = true });

            }
            catch (Exception ex)
            {

                throw;
            }
            //If we got this far, something failed, redisplay form
        }
        public virtual async Task<IActionResult> ThankYou(int id = 0)
        {
            var model = await _noticeBoardModelFactory.PrepareNoticeModelAsync(id);
            return View(model);
        }

        public virtual async Task<IActionResult> Display()
        {
            var model = await _noticeBoardModelFactory.PrepareNoticeModelAsync();
            if (model.Count() > 0)
            {
                return Json(new { success = true, result = model.Select(x => new { Id = x.Id }) });
            }
            return Json(new { success = false });
        }

        public virtual async Task<IActionResult> ShowInPage()
        {
            string currentUrl = string.Empty;
            var model = await _noticeBoardModelFactory.PrepareShowInOtherPagePopUpModelAsync(currentUrl);
            var relatedPageModel = model.Where(x => x.FormPopUpType == 2);
            var allPage = model.Where(x => x.FormPopUpType == 3);
            string browserUrl = HttpContext.Request.Headers["Referer"].ToString();
            var url = browserUrl.Split("/");
            List<NoticeBoardModel> filteredModel = new List<NoticeBoardModel>();
            if (url.Length > 0)
            {
                currentUrl = url[url.Length - 1];
                filteredModel = relatedPageModel.Where(x => x.RelatedPageURL is not null && x.RelatedPageURL.Split(',').Contains(currentUrl)).ToList();
            }
            filteredModel.AddRange(allPage.ToList());
            if (filteredModel.Count() > 0)
            {
                return Json(new { success = true, result = filteredModel.Select(x => new { Id = x.Id }) });
            }
            return Json(new { success = false });
        }

        public virtual async Task<IActionResult> GetIntervalActionAsync()
        {
            string browserUrl = HttpContext.Request.Headers["Referer"].ToString();
            var url = browserUrl.Split("/");
            var currentUrl = url[url.Length - 1];
            var model = await _noticeBoardModelFactory.PrepareShowIntervalModelAsync(currentUrl);

            if (model is not null)
            {
                return Json(new { success = true, result = model.Id, Timer = model.Timer });
            }
            return Json(new { success = false });
        }

    }
}

