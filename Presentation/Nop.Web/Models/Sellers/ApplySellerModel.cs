using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Sellers
{
    public partial record ApplySellerModel : BaseNopModel
    {
        public ApplySellerModel()
        {
            SellerAttributes = new List<SellerAttributeModel>();
        }

        [NopResourceDisplayName("Sellers.ApplyAccount.Name")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [NopResourceDisplayName("Sellers.ApplyAccount.Email")]
        [Required]
        public string Email { get; set; }

        [NopResourceDisplayName("Sellers.ApplyAccount.Description")]
        public string Description { get; set; }

        public IList<SellerAttributeModel> SellerAttributes { get; set; }

        public bool DisplayCaptcha { get; set; }

        public bool TermsOfServiceEnabled { get; set; }
        public bool TermsOfServicePopup { get; set; }

        public bool DisableFormInput { get; set; }
        public string Result { get; set; }
    }
}