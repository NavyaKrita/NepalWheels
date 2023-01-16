using Google.Protobuf.WellKnownTypes;
using Nop.Core.Domain.Notice;
using Nop.Services.Notice;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models;

using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Models.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Factories
{
    public partial class NoticeBoardModelFactory : INoticeBoardModelFactory
    {
        private readonly INoticeBoardService _noticeBoardService;
        public NoticeBoardModelFactory(INoticeBoardService noticeBoardService)
        {
            _noticeBoardService = noticeBoardService;
        }

        public virtual async Task<NoticeBoardModel> PrepareNoticeModelAsync(int id)
        {
            //get notices
            var notices = await _noticeBoardService.GetNoticeByIdAsync(id);
            //prepare list mode
            if (notices == null)
            {
                return
                new NoticeBoardModel();
            }
            return
                new NoticeBoardModel()
                {
                    Id = notices.Id,
                    Notice = notices.Notice,
                    DisplayForm = notices.DisplayForm,
                    PublishedTo = notices.PublishedTo,
                    PublishedFrom = notices.PublishedFrom,
                    ThankYou = notices.ThankYou,
                    Title = notices.Title
                };
        }

        public virtual async Task<IEnumerable<NoticeBoardModel>> PrepareNoticeModelAsync()
        {
            //get notices
            var noticesResult = await _noticeBoardService.GetNoticeByPublishedDateAsync();
            //prepare list mode
            if (noticesResult == null)
            {
                return
                 Enumerable.Empty<NoticeBoardModel>();
            }
            return
               noticesResult.Select(notices => new NoticeBoardModel()
               {
                   Id = notices.Id,
                   Notice = notices.Notice,
                   DisplayForm = notices.DisplayForm,
                   PublishedTo = notices.PublishedTo,
                   PublishedFrom = notices.PublishedFrom,
                   ThankYou = notices.ThankYou,
                   Title = notices.Title
               });
        }
    }
}
