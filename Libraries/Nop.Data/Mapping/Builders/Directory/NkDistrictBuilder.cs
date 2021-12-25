using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Directory;

using Nop.Data.Extensions;


namespace Nop.Data.Mapping.Builders.Directory
{

    public partial class NkDistrictBuilder : NopEntityBuilder<NkDistrict>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(NkDistrict.Name)).AsString(50)
                .WithColumn(nameof(NkDistrict.ProvinceId)).AsInt32().ForeignKey<StateProvince>();





        }

        #endregion
    }
}
