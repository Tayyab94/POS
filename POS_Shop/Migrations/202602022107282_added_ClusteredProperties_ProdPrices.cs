namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_ClusteredProperties_ProdPrices : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.ProductPrices", name: "IX_ProductId", newName: "IX_ProductPrices_ProductId");
            AlterColumn("dbo.ProductPrices", "TypeName", c => c.String(maxLength: 30));
            CreateIndex("dbo.ProductPrices", "TypeName", name: "IX_ProductPrices_TypeName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductPrices", "IX_ProductPrices_TypeName");
            AlterColumn("dbo.ProductPrices", "TypeName", c => c.String());
            RenameIndex(table: "dbo.ProductPrices", name: "IX_ProductPrices_ProductId", newName: "IX_ProductId");
        }
    }
}
