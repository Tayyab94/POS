namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_LincenseKyeMode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        LicenseKey = c.String(nullable: false, maxLength: 100),
                        MacAddress = c.String(nullable: false, maxLength: 50),
                        HardwareId = c.String(nullable: false, maxLength: 200),
                        LicenseType = c.Int(nullable: false),
                        IssueDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.LicenseKey, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Licenses", new[] { "LicenseKey" });
            DropTable("dbo.Licenses");
        }
    }
}
