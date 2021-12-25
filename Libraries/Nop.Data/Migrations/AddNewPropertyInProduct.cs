using FluentMigrator;
using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Migrations
{
    [NopMigration("2021/09/26 07:53:20", "Product. Add new property HasEMIAvailable InterestRate DownPaymentPercent")]
    public class AddNewPropertyInProduct : AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            Create.Column(nameof(Product.HasEMIAvailable))
            .OnTable(nameof(Product))
            .AsBoolean()
            .Nullable().WithDefaultValue(0);

            Create.Column(nameof(Product.DownPaymentPercent))
           .OnTable(nameof(Product))
           .AsDecimal()
           .Nullable().WithDefaultValue(0);

            Create.Column(nameof(Product.InterestRate))
           .OnTable(nameof(Product))
           .AsDecimal()
           .Nullable().WithDefaultValue(0);
        }
    }
}
