namespace POS_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editQty_decimal_TempOdrDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TempOrderDetails", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TempOrderDetails", "Quantity", c => c.Int(nullable: false));
        }
    }
}
