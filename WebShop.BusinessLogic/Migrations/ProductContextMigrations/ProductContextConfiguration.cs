using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel.Seed;

namespace WebShop.BusinessLogic.Migrations.ProductContextMigrations
{
    internal sealed class ProductContextConfiguration : DbMigrationsConfiguration<ProductContext>
    {
        public ProductContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ProductContextMigrations";
        }
    }
}
