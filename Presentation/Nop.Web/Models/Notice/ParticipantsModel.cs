using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Notice
{
    public partial record ParticipantsModel : BaseNopModel
    {

        [NopResourceDisplayName("FullName")]
        [Required]
        public string Name { get; set; }
        [NopResourceDisplayName("PhoneNumber")]
        [Required]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }
        [NopResourceDisplayName("City")]
        [Required]
        public string City { get; set; }      
        public string Notice { get; set; }
        public int NoticeBoardId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public bool IsDisplayForm { get; set; }
    }
}
