﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Notice;
using Nop.Services.Blogs;
using Nop.Services.Catalog;
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
        private readonly IBlogService _blogService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        public NoticeBoardModelFactory(INoticeBoardService noticeBoardService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IBlogService blogService)
        {
            _noticeBoardService = noticeBoardService;
            _manufacturerService = manufacturerService;
            _productService = productService;
            _blogService = blogService;
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
                 return notices.Select(notice =>
                {
                    var noticeModel = new NoticeBoardModel()
                    {
                        Id = notice.Id,
                        Notice = notice.Notice,
                        DisplayForm = notice.DisplayForm,
                        PublishedTo = notice.PublishedTo,
                        PublishedFrom = notice.PublishedFrom,
                        ThankYou = notice.ThankYou,
                        Title = notice.Title,
                        TermsAndCondition = notice.TermsAndCondition,
                    };
                    return noticeModel;
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

        public virtual async Task<CreateNoticeBoardModel> PrepareNoticeBoardModelAsync(CreateNoticeBoardModel model, NoticeBoard noticeBoard, bool excludeProperties = false)
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
                    model.Name = noticeBoard.Name;
                    model.PhoneNumber = noticeBoard.PhoneNumber;
                    model.EmailAddress = noticeBoard.EmailAddress;
                    model.Age = noticeBoard.Age;
                    model.Address = noticeBoard.Address;
                    model.City = noticeBoard.City;
                    model.TermsAndCondition = noticeBoard.TermsAndCondition;
                    model.ManufacturerId = noticeBoard.ManufacturerId;
                    model.BlogId = noticeBoard.BlogId;
                    model.BikeName = noticeBoard.BikeName;
                    model.CC = noticeBoard.CC;
                    model.SelectedProductIds = noticeBoard.Products.Split(',').Select(x => int.Parse(x.Trim())).ToList();
                    model.Products = await GetManufacturerFeaturedProductsAsync(model.ManufacturerId);
                    model.ButtonDisplayText = noticeBoard.ButtonDisplayText;
                }
            }
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
            var manufacturersList = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            manufacturersList.Add(new SelectListItem { Text = "", Value = "0" });
            foreach (var item in manufacturers)
            {
                manufacturersList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            var blogs = await _blogService.GetAllBlogPostsAsync();

            var mblogsList = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            mblogsList.Add(new SelectListItem { Text = "", Value = "0" });
            foreach (var item in blogs)
            {
                mblogsList.Add(new SelectListItem
                {
                    Text = item.Title,
                    Value = item.Id.ToString()
                });
            }
            model.Blogs = mblogsList;
            model.Manufacturers = manufacturersList;
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
            var notices = await _noticeBoardService.GetAllParticipatesAsync(searchModel.Lead,
                searchModel.LeadGeneratedFrom,
                searchModel.StartDate,
                searchModel.EndDate,
                pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new NoticeBoardDetailListModel().PrepareToGrid(searchModel, notices, () =>
            {
                //fill in model values from the entity
                return notices.Select(noticeBoard =>
                {
                    var notice = new NoticeBoardDetailModel()
                    {
                        Id = noticeBoard.Id,
                        Notice = noticeBoard.Notice,
                        Name = noticeBoard.Name,
                        PhoneNumber = noticeBoard.PhoneNumber,
                        EmailAddress = noticeBoard.EmailAddress,
                        Age = noticeBoard.Age,
                        Address = noticeBoard.Address,
                        City = noticeBoard.City,
                        BikeName = noticeBoard.BikeName,
                        CC = noticeBoard.CC,
                        Lead = noticeBoard.Category,
                        Module = noticeBoard.LeadGenerate
                    };
                    return notice;
                });
            });

            return model;
        }


        public async Task<ParticipantsCreateModel> PrepareNoticeModelAsync(int blogId)
        {
            var model = new ParticipantsCreateModel();
            var notices = await _noticeBoardService.GetNoticeByBlogIdAsync(blogId);
            if (notices is not null)
            {
                var productIds = notices.Products.Split(',').Select(x => int.Parse(x.Trim())).ToArray();

                var products = await _productService.GetProductsByIdsAsync(productIds);
                var productsList = new List<SelectListItem>();
                //clone the list to ensure that "selected" property is not set
                productsList.Add(new SelectListItem { Text = "", Value = "" });
                foreach (var item in products)
                {
                    productsList.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Name.ToString()
                    });
                }
                model.Products = productsList;
                model.ManufacturerId = notices.ManufacturerId;
            }

            return model;
        }
        public async Task<IEnumerable<SelectListItem>> GetManufacturerFeaturedProductsAsync(int manufacturerId)
        {
            var products = await _productService.GetManufacturerFeaturedProductsAsync(manufacturerId);
            var productsList = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            productsList.Add(new SelectListItem { Text = "", Value = "0" });
            foreach (var item in products)
            {
                productsList.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            return productsList;
        }
    }
}
