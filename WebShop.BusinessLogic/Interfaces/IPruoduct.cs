﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        bool IsProductValid(int id);
    }
}
