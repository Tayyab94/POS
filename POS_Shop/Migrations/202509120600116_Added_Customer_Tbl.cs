namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Customer_Tbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        CustomerAddress = c.String(nullable: false),
                        ContactNo = c.String(nullable: false),
                        CityId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CityId", "dbo.Cities");
            DropIndex("dbo.Customers", new[] { "CityId" });
            DropTable("dbo.Customers");
        }
    }
}
