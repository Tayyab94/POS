namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class licenseModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Licenses", new[] { "LicenseKey" });
            CreateIndex("dbo.Licenses", "MacAddress");
            CreateIndex("dbo.Licenses", "HardwareId");
            CreateIndex("dbo.Licenses", "IsActive");
            DropColumn("dbo.Licenses", "LicenseKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Licenses", "LicenseKey", c => c.String(nullable: false, maxLength: 100));
            DropIndex("dbo.Licenses", new[] { "IsActive" });
            DropIndex("dbo.Licenses", new[] { "HardwareId" });
            DropIndex("dbo.Licenses", new[] { "MacAddress" });
            CreateIndex("dbo.Licenses", "LicenseKey", unique: true);
        }
    }
}
