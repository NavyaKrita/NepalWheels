using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;

namespace Nop.Data.Mapping.Builders.Vendors
{
    /// <summary>
    /// Represents a vendor entity builder
    /// </summary>
    public partial class NkVendorLocationBuilder : NopEntityBuilder<NkVendorLocation>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(NkVendorLocation.ManufacturerId)).AsInt32().NotNullable()
                .WithColumn(nameof(NkVendorLocation.DistrictId)).AsInt32().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Address)).AsString().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Genre)).AsInt32().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Maplocation)).AsString().Nullable()
                .WithColumn(nameof(NkVendorLocation.ContactPerson)).AsString().NotNullable()
                .WithColumn(nameof(NkVendorLocation.CategoryId)).AsInt32().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Published)).AsBoolean().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Deleted)).AsBoolean().NotNullable()
                .WithColumn(nameof(NkVendorLocation.PhoneNo)).AsString().NotNullable()
                .WithColumn(nameof(NkVendorLocation.Email)).AsString().NotNullable()
                .WithColumn(nameof(NkVendorLocation.DealerName)).AsString().NotNullable()
                .WithColumn(nameof(NkVendorLocation.MobileNo)).AsString().NotNullable();





        }

        #endregion
    }
}