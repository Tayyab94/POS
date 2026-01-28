namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Prod_Tbl_addedProdPrice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Prod_Unit_TypeId = c.Int(nullable: false),
                        TypeName = c.String(),
                        Unit = c.String(),
                        ItemsCount = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductUnits", t => t.Prod_Unit_TypeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Prod_Unit_TypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPrices", "Prod_Unit_TypeId", "dbo.ProductUnits");
            DropForeignKey("dbo.ProductPrices", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductPrices", new[] { "Prod_Unit_TypeId" });
            DropIndex("dbo.ProductPrices", new[] { "ProductId" });
            DropTable("dbo.ProductPrices");
        }
    }
}
