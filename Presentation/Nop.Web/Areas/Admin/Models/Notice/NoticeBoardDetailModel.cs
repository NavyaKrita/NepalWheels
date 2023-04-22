using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Age")]
       
        public int Age { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.BikeName")]
        public string BikeName { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.CC")]
        public string CC { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.LeadGeneratedFrom")]
        public string Module { get; set; }

        [NopResourceDisplayName("Admin.NoticeBoard.Fields.Lead")]
        public string Lead { get; set; }

    }


}
