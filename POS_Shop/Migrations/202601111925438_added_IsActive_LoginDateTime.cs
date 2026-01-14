namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_IsActive_LoginDateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthUsers", "Role", c => c.Int(nullable: false));
            AddColumn("dbo.AuthUsers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.AuthUsers", "LastLogin", c => c.DateTime());
            DropColumn("dbo.AuthUsers", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthUsers", "UserRole", c => c.String());
            DropColumn("dbo.AuthUsers", "LastLogin");
            DropColumn("dbo.AuthUsers", "IsActive");
            DropColumn("dbo.AuthUsers", "Role");
        }
    }
}
