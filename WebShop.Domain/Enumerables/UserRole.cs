using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Domain.Enumerables
{
    public enum UserRole
    {
        None = 0,
        Guest = 1,
        User = 10,
        Admin = 100,
        Banned = 200
        
    }
}
