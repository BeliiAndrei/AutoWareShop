using System;
using WebShop.BusinessLogic.Core;
using WebShop.BusinessLogic.Interfaces;
using WebShop.Domain.User.Admin;
using WebShop.Domain.User.Auth;
using WebShop.Domain.User.Delivery;
using WebShop.Domain.User.Modify;
using WebShop.Domain.User.Registration;

namespace WebShop.BusinessLogic.BLogic
{
    public class UserBL : UserApi, IUser
    {
        public bool ChangePasswordInDB(ChangePasswordClass pass)
        {
            return ChangePasswordInDBAction(pass);
        }

        public UserInfo EditUserProfile(UserInfo data)
        {
            var user = EditUserProfileAction(data);

            var model = new UserInfo()
            {
                Id = data.Id,
                UserName = data.UserName,
                UserLastName = data.UserLastName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                Role = data.Role,
                Balance = data.Balance,
            };
            return model;
        }

        public UserInfo GetUserIdBySessionKey(string sessionKey)
        {
            return GetUserIdBySessionKeyAction(sessionKey);
        }

        public UserInfo GetUserInfoById(int id)
        {
            throw new NotImplementedException();
        }

        public UserRegistrationResponse UserRegistrationAction(UserLoginData data)
        {
            throw new NotImplementedException();
        }

        int IUser.GetUserIdBySessionKey(string sessionKey)
        {
            throw new NotImplementedException();
        }

        UserLoginResponse IUser.UserLoginAction(UserLoginData auth)
        {
            return UserLoginAction(auth);
        }

        UserRegistrationResponse IUser.UserRegistrationAction(UserRegistrationData data)
        {
            return UserRegistrationAction(data);
        }
        public bool IsProductValid(int id)
        {
            return IsProductValidAction(id);
        }

        public bool AddDeliveryAddress(DeliveryL address, int userId)
        {
            throw new NotImplementedException();
        }

        public DeliveryL GetDeliveryAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditDeliveryAddress(DeliveryL address)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDeliveryAddress(int id)
        {
            throw new NotImplementedException();
        }
    }
}
