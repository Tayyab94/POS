namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_productTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductEnglishName = c.String(),
                        ProductUrduName = c.String(),
                        ProductType = c.String(),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Int(nullable: false),
                        SubcategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubCategories", t => t.SubcategoryId)
                .Index(t => t.SubcategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubcategoryId", "dbo.SubCategories");
            DropIndex("dbo.Products", new[] { "SubcategoryId" });
            DropTable("dbo.Products");
        }
    }
}
