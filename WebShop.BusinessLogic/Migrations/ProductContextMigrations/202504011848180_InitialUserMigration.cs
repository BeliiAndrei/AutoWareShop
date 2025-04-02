namespace WebShop.BusinessLogic.Migrations.ProductContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUserMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDBTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Producer = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Article = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductDBTables");
        }
    }
}
