using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.BusinessLogic.DBModel;

namespace WebShop.BusinessLogic.Migrations.UserContextMigrations
{
    internal sealed class UserContextConfiguration : DbMigrationsConfiguration<UserContext>
    {
        public UserContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\UserContextMigrations";
        }
    }
}
