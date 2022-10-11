using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Sellers;
using Nop.Services.Common;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Sellers;
using Nop.Web.Models.Sellers;

namespace Nop.Web.Factories
{
    /// <summary>
    /// Represents the seller model factory
    /// </summary>
    public partial class SellerModelFactory : ISellerModelFactory
    {
        #region Fields

        private readonly CaptchaSettings _captchaSettings;
        private readonly CommonSettings _commonSettings;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISellerAttributeParser _sellerAttributeParser;
        private readonly ISellerAttributeService _sellerAttributeService;
        private readonly IWorkContext _workContext;
        private readonly MediaSettings _mediaSettings;
        private readonly SellerSettings _sellerSettings;

        #endregion

        #region Ctor

        public SellerModelFactory(CaptchaSettings captchaSettings,
            CommonSettings commonSettings,
            IGenericAttributeService genericAttributeService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISellerAttributeParser sellerAttributeParser,
            ISellerAttributeService sellerAttributeService,
            IWorkContext workContext,
            MediaSettings mediaSettings,
            SellerSettings sellerSettings)
        {
            _captchaSettings = captchaSettings;
            _commonSettings = commonSettings;
            _genericAttributeService = genericAttributeService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _sellerAttributeParser = sellerAttributeParser;
            _sellerAttributeService = sellerAttributeService;
            _workContext = workContext;
            _mediaSettings = mediaSettings;
            _sellerSettings = sellerSettings;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare seller attribute models
        /// </summary>
        /// <param name="sellerAttributesXml">Seller attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of the seller attribute model
        /// </returns>
        protected virtual async Task<IList<SellerAttributeModel>> PrepareSellerAttributesAsync(string sellerAttributesXml)
        {
            var result = new List<SellerAttributeModel>();

            var sellerAttributes = await _sellerAttributeService.GetAllSellerAttributesAsync();
            foreach (var attribute in sellerAttributes)
            {
                var attributeModel = new SellerAttributeModel
                {
                    Id = attribute.Id,
                    Name = await _localizationService.GetLocalizedAsync(attribute, x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = await _sellerAttributeService.GetSellerAttributeValuesAsync(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var valueModel = new SellerAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = await _localizationService.GetLocalizedAsync(attributeValue, x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(valueModel);
                    }
                }

                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!string.IsNullOrEmpty(sellerAttributesXml))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = await _sellerAttributeParser.ParseSellerAttributeValuesAsync(sellerAttributesXml);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!string.IsNullOrEmpty(sellerAttributesXml))
                            {
                                var enteredText = _sellerAttributeParser.ParseValues(sellerAttributesXml, attribute.Id);
                                if (enteredText.Any())
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                result.Add(attributeModel);
            }

            return result;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare the apply seller model
        /// </summary>
        /// <param name="model">The apply seller model</param>
        /// <param name="validateSeller">Whether to validate that the customer is already a seller</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="sellerAttributesXml">Seller attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the apply seller model
        /// </returns>
        public virtual async Task<ApplySellerModel> PrepareApplySellerModelAsync(ApplySellerModel model,
            bool validateSeller, bool excludeProperties, string sellerAttributesXml)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (validateSeller && (await _workContext.GetCurrentCustomerAsync()).SellerId > 0)
            {
                //already applied for seller account
                model.DisableFormInput = true;
                model.Result = await _localizationService.GetResourceAsync("Sellers.ApplyAccount.AlreadyApplied");
            }

            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnApplySellerPage;
            model.TermsOfServiceEnabled = _sellerSettings.TermsOfServiceEnabled;
            model.TermsOfServicePopup = _commonSettings.PopupForTermsOfServiceLinks;

            if (!excludeProperties)
            {
                model.Email = (await _workContext.GetCurrentCustomerAsync()).Email;
            }

            //seller attributes
            model.SellerAttributes = await PrepareSellerAttributesAsync(sellerAttributesXml);

            return model;
        }

        /// <summary>
        /// Prepare the seller info model
        /// </summary>
        /// <param name="model">Seller info model</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overriddenSellerAttributesXml">Overridden seller attributes in XML format; pass null to use SellerAttributes of seller</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the seller info model
        /// </returns>
        public virtual async Task<SellerInfoModel> PrepareSellerInfoModelAsync(SellerInfoModel model,
            bool excludeProperties, string overriddenSellerAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var seller = await _workContext.GetCurrentSellerAsync();
            if (!excludeProperties)
            {
                model.Description = seller.Description;
                model.Email = seller.Email;
                model.Name = seller.Name;
            }

            var picture = await _pictureService.GetPictureByIdAsync(seller.PictureId);
            var pictureSize = _mediaSettings.AvatarPictureSize;
            (model.PictureUrl, _) = picture != null ? await _pictureService.GetPictureUrlAsync(picture, pictureSize) : (string.Empty, null);

            //seller attributes
            if (string.IsNullOrEmpty(overriddenSellerAttributesXml))
                overriddenSellerAttributesXml = await _genericAttributeService.GetAttributeAsync<string>(seller, NopSellerDefaults.SellerAttributes);
            model.SellerAttributes = await PrepareSellerAttributesAsync(overriddenSellerAttributesXml);

            return model;
        }

        #endregion
    }
}