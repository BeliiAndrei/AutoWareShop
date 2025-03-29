namespace WebShop.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDBTables", "Test", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserDBTables", "Test");
        }
    }
}
