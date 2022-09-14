
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
               
        #endregion
    }
}
