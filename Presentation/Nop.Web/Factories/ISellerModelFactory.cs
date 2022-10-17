using System.Threading.Tasks;
using Nop.Web.Models.Sellers;

namespace Nop.Web.Factories
{
    /// <summary>
    /// Represents the interface of the seller model factory
    /// </summary>
    public partial interface ISellerModelFactory
    {
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
        Task<ApplySellerModel> PrepareApplySellerModelAsync(ApplySellerModel model, bool validateSeller, bool excludeProperties, string sellerAttributesXml);

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
        Task<SellerInfoModel> PrepareSellerInfoModelAsync(SellerInfoModel model, bool excludeProperties, string overriddenSellerAttributesXml = "");
    }
}