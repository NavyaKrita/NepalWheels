using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Notice
{
    public partial record NoticeBoardDetailSearchModel : BaseSearchModel
    {
        public DateTime? StartDate { get; set; } = DateTime.Now.Date.AddMonths(-1);
        public DateTime? EndDate { get; set; } = DateTime.Now.Date;

        public string Notice { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.LeadGeneratedFrom")]
        public string LeadGeneratedFrom { get; set; }
        public string Lead { get; set; }
    }
}
