﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.User.Auth;

namespace WebShop.BusinessLogic.Interfaces
{
    public interface ISession
    {
        UserLoginResponse UserLogin(UserLoginData data);

    }
}
