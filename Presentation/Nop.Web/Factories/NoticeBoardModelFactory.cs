using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Notice;
using Nop.Services.Blogs;
using Nop.Services.Catalog;
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
        private readonly IBlogService _blogService;
        private readonly IProductService _productService;
        public NoticeBoardModelFactory(INoticeBoardService noticeBoardService,
            IBlogService blogService,
            IProductService productService)
        {
            _noticeBoardService = noticeBoardService;
            _blogService = blogService;
            _productService = productService;
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
            else
            {

                var model = new NoticeBoardModel()
                {
                    Id = notices.Id,
                    Notice = notices.Notice,
                    DisplayForm = notices.OpenFormInPopUp,
                    PublishedTo = notices.PublishedTo,
                    PublishedFrom = notices.PublishedFrom,
                    ThankYou = notices.ThankYou,
                    Title = notices.Title,
                    TermAndConditions = notices.TermsAndCondition,
                    ButtonDisplayText = notices.ButtonDisplayText,
                    URL = notices.RedirectionURL,
                    InURL = notices.InURL,
                    DisplayPopUpInSamePage = notices.DisplayPopUpInSamePage,
                    FormPopUpType = notices.FormPopUpType,
                    RedirectionURL = notices.RedirectionURL,
                    Timer = notices.Timer,
                    NoticeField = new()
                    {
                        Name = notices.Name,
                        PhoneNumber = notices.PhoneNumber,
                        EmailAddress = notices.EmailAddress,
                        Age = notices.Age,
                        Address = notices.Address,
                        City = notices.City,
                        BikeName = notices.BikeName,
                        CC = notices.CC
                    }
                };
                var productIds = notices.Products.Split(',').Select(x => int.Parse(x.Trim())).ToArray();

                var products = await _productService.GetProductsByIdsAsync(productIds);
                var productsList = new List<SelectListItem>();
                //clone the list to ensure that "selected" property is not set
                productsList.Add(new SelectListItem { Text = "Choose your Option", Value = "0" });
                foreach (var item in products)
                {
                    productsList.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name.ToString()
                    });
                }
                model.Products = productsList;
                return model;
            }

        }

        public virtual async Task<IEnumerable<NoticeBoardModel>> PrepareNoticeModelAsync()
        {
            //get notices
            var noticesResult = await _noticeBoardService.GetHomePageNoticeByPublishedDateAsync();
            //prepare list mode
            if (noticesResult == null)
            {
                return
                 Enumerable.Empty<NoticeBoardModel>();
            }
            return
               noticesResult.Select(notices => new NoticeBoardModel()
               {
                   Id = notices.Id
               });
        }

        public virtual async Task<IEnumerable<NoticeBoardModel>> PrepareShowInOtherPagePopUpModelAsync(string url)
        {
            //get notices
            var noticesResult = await _noticeBoardService.GetNoticeByPublishedDateAsync(url);
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
                   FormPopUpType = notices.FormPopUpType,
                   RelatedPageURL = notices.RelatedPageURL,
                   DisplayPopUpInSamePage = notices.DisplayPopUpInSamePage,
                   Timer = notices.Timer,
                   RedirectionURL = notices.RedirectionURL
               });
        }

        public virtual async Task<NoticeBoardModel> PrepareShowIntervalModelAsync(string url)
        {
            //get notices
            var notices = await _noticeBoardService.GetNoticeByIntervalAsync(url);
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
                    FormPopUpType = notices.FormPopUpType,
                    RelatedPageURL = notices.RelatedPageURL,
                    DisplayPopUpInSamePage = notices.DisplayPopUpInSamePage,
                    Timer = notices.Timer,
                    RedirectionURL = notices.RedirectionURL
                };
        }
    }
}
