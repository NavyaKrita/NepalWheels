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



        public virtual async Task<NoticeBoardModel> PrepareNoticeModelAsync()
        {
            //get notices
            var notices = await _noticeBoardService.GetNoticeByPublishedDateAsync();
            //prepare list mode
            if (notices == null)
            {
                return
                 new NoticeBoardModel();
            }
            return
                 new NoticeBoardModel()
                 {
                    
                     Notice = notices.Notice,
                     DisplayForm = notices.DisplayForm,
                     PublishedTo = notices.PublishedTo,
                     PublishedFrom = notices.PublishedFrom,
                     ThankYou = notices.ThankYou,
                     Title = notices.Title
                 };
        }
    }
}
