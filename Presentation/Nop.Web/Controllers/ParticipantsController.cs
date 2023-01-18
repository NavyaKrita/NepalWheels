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
            ParticipantsModel model = new();
            model.NoticeBoardId = notice.Id;
            model.Notice = notice.Notice;
            model.IsDisplayForm = notice.DisplayForm;
            return View(model);
        }

        [HttpPost]

        /// <returns>A task that represents the asynchronous operation</returns>
        public virtual async Task<IActionResult> RegisterParticipants(ParticipantsModel model, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                NoticeBoardDetail detail = new();

                detail.CreatedOnUtc = DateTime.UtcNow;
                detail.EmailAddress = model.EmailAddress;
                detail.Name = model.Name;
                detail.PhoneNumber = model.PhoneNumber;
                detail.City = model.City;
                var latestNotice = await _noticeBoardService.GetNoticeByIdAsync(model.NoticeBoardId);
                detail.Notice = latestNotice?.Title;
                detail.NoticeBoardId = latestNotice?.Id;
                await _noticeBoardService.InsertNoticeParticipatesAsync(detail);

                return Json(new { success = true });
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

