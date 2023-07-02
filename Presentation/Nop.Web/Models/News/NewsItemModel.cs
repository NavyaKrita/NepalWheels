using System;
using System.Collections.Generic;
using Nop.Web.Framework.Models;
using Nop.Web.Models.Media;

namespace Nop.Web.Models.News
{
    public partial record NewsItemModel : BaseNopEntityModel
    {
        public NewsItemModel()
        {
            Comments = new List<NewsCommentModel>();
            AddNewComment = new AddNewsCommentModel();
        }

        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public string Title { get; set; }
        public string Short { get; set; }
        public string Full { get; set; }
        public bool AllowComments { get; set; }
        public int NumberOfComments { get; set; }
        public DateTime CreatedOn { get; set; }

        public IList<NewsCommentModel> Comments { get; set; }
        public AddNewsCommentModel AddNewComment { get; set; }
        public bool AllowLikes { get; set; }
        public IList<NewsLikeModel> Likes { get; set; }
        public NewsLikeModel AddLikes { get; set; }
        public IList<NewsCategoryModel> NewsCategories { get; set; }
        public PictureModel PictureModel { get; set; }
    }
    public partial record NewsCategoryModel : BaseNopEntityModel
    {
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool Published { get; set; }
        public bool LimitedToStores { get; set; }
        
        public string SeName { get; set; }

    }
}