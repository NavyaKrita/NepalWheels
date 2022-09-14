using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.Models.Notice
{
    public partial record NoticeBoardDetailModel: BaseNopEntityModel
    {
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Name")]
        public string Name { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.PhoneNumber")]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.EmailAddress")]
        public string EmailAddress { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.City")]
        public string City { get; set; }
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.NoticeBoard")]
        public string Notice { get; set; }
        public int NoticeBoardId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
