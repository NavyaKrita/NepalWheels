using Microsoft.AspNetCore.Mvc.Rendering;
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
        //[Required]
        public string Name { get; set; }
        [NopResourceDisplayName("PhoneNumber")]
        //[Required]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        //[Required]
        public string EmailAddress { get; set; }
        [NopResourceDisplayName("City")]
        //[Required]
        public string City { get; set; }

        [NopResourceDisplayName("Age")]
        [MaxLength(2)]
        [DataType(DataType.CreditCard)]
        [RegularExpression("([1-9][0-9]*)")]
        public string Age { get; set; }

        [NopResourceDisplayName("Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("BikeName")]
        public string BikeName { get; set; }

        [NopResourceDisplayName("CC")]
        public string CC { get; set; }
        public string Notice { get; set; }
        public int NoticeBoardId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        
        public string Title { get; set; }
        public string ThankYou { get; set; }
        public string TermAndConditions { get; set; }
        public ParticipantFieldModel ParticipantField { get; set; }
        public bool Agree { get; set; }
        [NopResourceDisplayName("Products")]
        public string ProductId { get; set; }
        [NopResourceDisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public string ButtonDisplayText { get; set; }
        public string URL { get; set; }

        public string RedirectionURL { get; set; }


        public string InURL { get; set; }

        public int? FormPopUpType { get; set; }
        public bool OpenFormInPopUp { get; set; }

        public bool DisplayPopUpInSamePage { get; set; }

        public string RelatedPageURL { get; set; }

        public string Timer { get; set; }
    }
    public partial record ParticipantFieldModel
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


    public partial record ParticipantSetupModel : BaseNopModel
    {

        [NopResourceDisplayName("FullName")]

        public string Name { get; set; }
        [NopResourceDisplayName("PhoneNumber")]

        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [NopResourceDisplayName("City")]
        public string City { get; set; }

        [NopResourceDisplayName("Age")]
        [MaxLength(2)]
        [DataType(DataType.CreditCard)]
        [RegularExpression("([1-9][0-9]*)")]
        public string Age { get; set; }

        [NopResourceDisplayName("Address")]
        public string Address { get; set; }

        [NopResourceDisplayName("BikeName")]
        public string BikeName { get; set; }

        [NopResourceDisplayName("CC")]
        public string CC { get; set; }

        public bool Agree { get; set; }
    }

    public partial record ParticipantModel : BaseNopModel
    {

        [NopResourceDisplayName("FullName")]

        public string Name { get; set; }
        [NopResourceDisplayName("PhoneNumber")]

        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [NopResourceDisplayName("EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [NopResourceDisplayName("City")]

        public string City { get; set; }

        [NopResourceDisplayName("Age")]
        [MaxLength(2)]
        [DataType(DataType.CreditCard)]
        [RegularExpression("([1-9][0-9]*)")]

        public string Age { get; set; }

        [NopResourceDisplayName("Address")]

        public string Address { get; set; }

        [NopResourceDisplayName("BikeName")]

        public string BikeName { get; set; }

        [NopResourceDisplayName("CC")]

        public string CC { get; set; }
        public string Module { get; set; }

        [NopResourceDisplayName("Products")]
        public string ProductId { get; set; }
        [NopResourceDisplayName("Manufacturer")]
        public int ManufacturerId { get; set; }

    }
}
