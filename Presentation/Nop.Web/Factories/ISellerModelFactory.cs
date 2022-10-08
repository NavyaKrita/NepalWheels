using System.Threading.Tasks;
using Nop.Web.Models.Sellers;

namespace Nop.Web.Factories
{
    /// <summary>
    /// Represents the interface of the vendor model factory
    /// </summary>
    public partial interface ISellerModelFactory
    {
        /// <summary>
        /// Prepare the apply vendor model
        /// </summary>
        /// <param name="model">The apply vendor model</param>
        /// <param name="validateSeller">Whether to validate that the customer is already a vendor</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="vendorAttributesXml">Seller attributes in XML format</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the apply vendor model
        /// </returns>
        Task<ApplySellerModel> PrepareApplySellerModelAsync(ApplySellerModel model, bool validateSeller, bool excludeProperties, string vendorAttributesXml);

        /// <summary>
        /// Prepare the vendor info model
        /// </summary>
        /// <param name="model">Seller info model</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="overriddenSellerAttributesXml">Overridden vendor attributes in XML format; pass null to use SellerAttributes of vendor</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the vendor info model
        /// </returns>
        Task<SellerInfoModel> PrepareSellerInfoModelAsync(SellerInfoModel model, bool excludeProperties, string overriddenSellerAttributesXml = "");
    }
}