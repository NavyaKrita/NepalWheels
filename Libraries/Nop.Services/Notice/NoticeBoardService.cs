﻿using Nop.Core;
using Nop.Core.Domain.Notice;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Notice
{
    public partial class NoticeBoardService : INoticeBoardService
    {
        private readonly IRepository<NoticeBoard> _noticeBoardRepository;
        private readonly IRepository<NoticeBoardDetail> _noticeBoardDetailRepository;
        public NoticeBoardService(IRepository<NoticeBoard> noticeBoardRepository,
           IRepository<NoticeBoardDetail> noticeBoardDetailRepository)
        {
            _noticeBoardRepository = noticeBoardRepository;
            _noticeBoardDetailRepository = noticeBoardDetailRepository;
        }

        public virtual async Task<IPagedList<NoticeBoard>> GetAllNoticeAsync(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _noticeBoardRepository.Table;

            query = query.OrderBy(v => v.CreatedOnUtc).ThenBy(v => v.Id);

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }


        public virtual async Task<IPagedList<NoticeBoardDetail>> GetAllParticipatesAsync(string notice, DateTime? startDate, DateTime? endDate, int pageIndex = 0, int pageSize = int.MaxValue)
        {

            var query = _noticeBoardDetailRepository.Table;

            query = query.OrderBy(v => v.CreatedOnUtc).ThenBy(v => v.Id);
            if (!string.IsNullOrEmpty(notice))
            {
                query = query.Where(c => c.Notice.Contains(notice));
            }
            if ((startDate != null && endDate != null) && (startDate <= endDate))
            {
                query = query.Where(a => a.CreatedOnUtc.Date >= startDate && a.CreatedOnUtc.Date <= endDate);
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public virtual async Task<NoticeBoard> GetNoticeByIdAsync(int noticeId)
        {
            return await _noticeBoardRepository.GetByIdAsync(noticeId);
        }

        public virtual async Task<IEnumerable<NoticeBoard>> GetNoticeByPublishedDateAsync()
        {
            DateTime today = DateTime.Now.Date;
            var query = _noticeBoardRepository.Table;

            query = query.Where(c => c.PublishedFrom <= today && c.PublishedTo >= today).OrderByDescending(t => t.PublishedFrom);
            return await query.ToListAsync();
        }

        public virtual async Task InsertNoticeAsync(NoticeBoard notice)
        {
            await _noticeBoardRepository.InsertAsync(notice);
        }

        public virtual async Task InsertNoticeParticipatesAsync(NoticeBoardDetail notice)
        {
            await _noticeBoardDetailRepository.InsertAsync(notice);
        }

        public virtual async Task UpdateNoticeAsync(NoticeBoard notice)
        {
            await _noticeBoardRepository.UpdateAsync(notice);
        }
    }
}
