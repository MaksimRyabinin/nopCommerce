using FluentMigrator;
using Nop.Core.Domain.Catalog;

namespace Nop.Data.Migrations
{
    [NopMigration("2020/06/11 11:24:16:2551770", "Product. Add author property")]
    public class AddAuthorProperty: AutoReversingMigration
    {
        /// <summary>Collect the UP migration expressions</summary>
        public override void Up()
        {
            Create.Column(nameof(Product.Author))
            .OnTable(nameof(Product))
            .AsString(255)
            .Nullable();
        }
    }
}
