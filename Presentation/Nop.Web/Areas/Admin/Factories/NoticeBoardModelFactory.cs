using Nop.Core.Domain.Notice;
using Nop.Services.Notice;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Areas.Admin.Models;
using Nop.Web.Areas.Admin.Models.Notice;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Factories
{
    public partial class NoticeBoardModelFactory : INoticeBoardModelFactory
    {
        private readonly INoticeBoardService _noticeBoardService;
        public NoticeBoardModelFactory(INoticeBoardService noticeBoardService)
        {
            _noticeBoardService = noticeBoardService;
        }
        public virtual async Task<NoticeBoardListModel> PrepareNoticeBoardListModelAsync(NoticeBoardSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get notices
            var notices = await _noticeBoardService.GetAllNoticeAsync(
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NoticeBoardListModel().PrepareToGrid(searchModel, notices, () =>
             {
               //fill in model values from the entity
               return notices.Select(vendor =>
              {
                  var vendorModel = new NoticeBoardModel()
                  {
                      Id = vendor.Id,
                      Notice = vendor.Notice,
                      DisplayForm = vendor.DisplayForm,
                      PublishedTo = vendor.PublishedTo,
                      PublishedFrom = vendor.PublishedFrom,
                      ThankYou = vendor.ThankYou,
                      Title = vendor.Title
                  };
                  return vendorModel;
              });
           });

            return model;
        }

        public virtual Task<NoticeBoardSearchModel> PrepareNoticeBoardSearchModelAsync(NoticeBoardSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }

        public virtual  CreateNoticeBoardModel PrepareNoticeBoardModelAsync(CreateNoticeBoardModel model, NoticeBoard noticeBoard, bool excludeProperties = false)
        {

            if (noticeBoard != null)
            {
                //fill in model values from the entity
                if (model == null)
                {
                    model = new CreateNoticeBoardModel();

                    model.Id = noticeBoard.Id;
                    model.Title = noticeBoard.Title;
                    model.Notice = noticeBoard.Notice;
                    model.DisplayForm = noticeBoard.DisplayForm;
                    model.PublishedFrom = noticeBoard.PublishedFrom;
                    model.PublishedTo = noticeBoard.PublishedTo;
                    model.ThankYou = noticeBoard.ThankYou;
                }
            }
           
            return model;
        }

        public Task<NoticeBoardDetailSearchModel> PrepareParticipantsSearchModelAsync(NoticeBoardDetailSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //prepare page parameters
            searchModel.SetGridPageSize();

            return Task.FromResult(searchModel);
        }

        public virtual async Task<NoticeBoardDetailListModel> PrepareParticipantsListModelAsync(NoticeBoardDetailSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get notices
            var notices = await _noticeBoardService.GetAllParticipatesAsync(searchModel.Notice, 
                searchModel.StartDate, 
                searchModel.EndDate,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NoticeBoardDetailListModel().PrepareToGrid(searchModel, notices, () =>
            {
                //fill in model values from the entity
                return notices.Select(notice =>
                {
                    var vendorModel = new NoticeBoardDetailModel()
                    {
                        Id = notice.Id,
                        Notice = notice.Notice,
                        Name = notice.Name,
                        PhoneNumber = notice.PhoneNumber,
                        CreatedOnUtc = notice.CreatedOnUtc,
                        EmailAddress = notice.EmailAddress,
                        City = notice.City
                    };
                    return vendorModel;
                });
            });

            return model;
        }
    }
}
