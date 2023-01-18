using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Models;

namespace Nop.Web.Models.Sellers
{
    public record SellerInfoModel : BaseNopModel
    {
        public SellerInfoModel()
        {
            SellerAttributes = new List<SellerAttributeModel>();
        }

        [NopResourceDisplayName("Account.SellerInfo.Name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [NopResourceDisplayName("Account.SellerInfo.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Account.SellerInfo.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Account.SellerInfo.Picture")]
        public string PictureUrl { get; set; }

        public IList<SellerAttributeModel> SellerAttributes { get; set; }
    }
}