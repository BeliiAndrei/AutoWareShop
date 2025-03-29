namespace WebShop.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserDBTables", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDBTables", "Test", c => c.String(maxLength: 20));
        }
    }
}
