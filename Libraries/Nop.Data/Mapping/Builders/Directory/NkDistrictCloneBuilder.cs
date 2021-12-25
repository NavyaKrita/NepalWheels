using FluentMigrator.Builders.Create.Table;
using Nop.Core.Domain.Directory;

using Nop.Data.Extensions;


namespace Nop.Data.Mapping.Builders.Directory
{

    public partial class NkDistrictCloneBuilder : NopEntityBuilder<NkDistrictClone>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(NkDistrictClone.Name)).AsString(50)
                .WithColumn(nameof(NkDistrictClone.ProvinceId)).AsInt32().ForeignKey<StateProvince>();





        }

        #endregion
    }
}
