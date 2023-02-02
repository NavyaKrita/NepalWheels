using LinqToDB.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Notice;
using Nop.Services.Notice;
using Nop.Web.Factories;
using Nop.Web.Models.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public partial class ParticipantsController : BasePublicController
    {
        private readonly INoticeBoardService _noticeBoardService;
        private readonly INoticeBoardModelFactory _noticeBoardModelFactory;
        public ParticipantsController(INoticeBoardService noticeBoardService,
           INoticeBoardModelFactory noticeBoardModelFactory)
        {
            _noticeBoardService = noticeBoardService;
            _noticeBoardModelFactory = noticeBoardModelFactory;
        }
        public virtual async Task<IActionResult> RegisterParticipants(int id)
        {
            var notice = await _noticeBoardModelFactory.PrepareNoticeModelAsync(id);
            ParticipantsModel model = new()
            {
                NoticeBoardId = notice.Id,
                Notice = notice.Notice,
                Title = notice.Title,
                IsDisplayForm = notice.DisplayForm,
                TermAndConditions = notice.TermAndConditions,
                ThankYou = notice.ThankYou,
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
            else if (latestNotice.BikeName && string.IsNullOrEmpty(model.BikeName))
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
                };


                detail.Notice = latestNotice?.Title;
                detail.NoticeBoardId = latestNotice?.Id;
                await _noticeBoardService.InsertNoticeParticipatesAsync(detail);

                return Json(new { success = true, displayThankYou = !string.IsNullOrEmpty(latestNotice.ThankYou) });
            }

            //If we got this far, something failed, redisplay form


            return Json(new { success = false });
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
                return Json(new { success = true, result = model });
            }
            return Json(new { success = false });
        }
    }
}

