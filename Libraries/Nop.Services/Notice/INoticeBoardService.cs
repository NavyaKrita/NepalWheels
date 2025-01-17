﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Notice;
namespace Nop.Services.Notice
{
    public partial interface INoticeBoardService
    {
        Task<IPagedList<NoticeBoard>> GetAllNoticeAsync(int pageIndex = 0, int pageSize = int.MaxValue);
        Task<NoticeBoard> GetNoticeByIdAsync(int noticeId);

        Task InsertNoticeAsync(NoticeBoard notice);

        Task UpdateNoticeAsync(NoticeBoard notice);

        Task<IPagedList<NoticeBoardDetail>> GetAllParticipatesAsync(string notice, DateTime ?startDate, DateTime? endDate, int pageIndex = 0, int pageSize = int.MaxValue);
        Task InsertNoticeParticipatesAsync(NoticeBoardDetail notice);
        Task<IEnumerable<NoticeBoard>> GetNoticeByPublishedDateAsync();
    }
}
