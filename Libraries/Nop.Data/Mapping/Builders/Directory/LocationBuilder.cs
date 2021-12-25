using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Vendors;
using Nop.Data.Extensions;


namespace Nop.Data.Mapping.Builders.Directory
{

    public partial class LocationBuilder : NopEntityBuilder<Location>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(NkDistrict.Name)).AsString(50);
                





        }

        #endregion
    }
}
