namespace WebShop.BusinessLogic.Migrations.ProductContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDBTables", "ImageString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductDBTables", "ImageString");
        }
    }
}
