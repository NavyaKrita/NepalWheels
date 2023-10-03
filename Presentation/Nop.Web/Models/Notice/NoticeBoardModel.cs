
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Notice
{
    
    public partial record NoticeBoardModel : BaseNopModel
    {
        #region Ctor

        public NoticeBoardModel()
        {
           
        }

        #endregion

        #region Properties
        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Name")]
        public string Title { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Notice")]
        public string Notice { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.PublishedFrom")]
        public DateTime PublishedFrom { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.PublishedTo")]
        public DateTime PublishedTo { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.DisplayForm")]
        public bool DisplayForm { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.ThankYou")]
        public string ThankYou { get; set; }
        public string TermAndConditions { get; set; }
        public int Id { get; set; }
        public NoticeFieldModel NoticeField { get; set; }
        public string ButtonDisplayText { get; set; }
        public string URL { get; set; }

        public int ManufacturerId { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Product")]

        public IList<int> SelectedProductIds { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.DisplayURL")]

        public string RedirectionURL { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.ButtonText")]
      
        public string InURL { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.PopUpType")]
        public int? FormPopUpType { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.DisplayPopUpInSamePage")]
        public bool DisplayPopUpInSamePage { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.RelatedPageURL")]
        public string RelatedPageURL { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Timer")]
        public string Timer { get; set; }
        #endregion
    }
    public partial record NoticeFieldModel
    {
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Name")]
        public bool Name { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Phone")]
        public bool PhoneNumber { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Email")]
        public bool EmailAddress { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Age")]
        public bool Age { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Address")]
        public bool Address { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.City")]
        public bool City { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.BikeName")]
        public bool BikeName { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.CC")]
        public bool CC { get; set; }
    }
}
