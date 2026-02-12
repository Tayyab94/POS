namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Supplier_tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 50),
                        ShopName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 100),
                        ContactNo = c.String(nullable: false, maxLength: 20),
                        CityId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.SupplierName, name: "IX_Supplier_SupplierName")
                .Index(t => t.ShopName, name: "IX_Supplier_ShopName")
                .Index(t => t.ContactNo, name: "IX_Supplier_ContactNo")
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suppliers", "CityId", "dbo.Cities");
            DropIndex("dbo.Suppliers", new[] { "CityId" });
            DropIndex("dbo.Suppliers", "IX_Supplier_ContactNo");
            DropIndex("dbo.Suppliers", "IX_Supplier_ShopName");
            DropIndex("dbo.Suppliers", "IX_Supplier_SupplierName");
            DropTable("dbo.Suppliers");
        }
    }
}
