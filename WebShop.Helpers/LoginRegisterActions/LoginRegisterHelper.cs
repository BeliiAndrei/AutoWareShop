using System;
using System.Security.Cryptography;
using System.Text;

namespace WebShop.Helpers.LoginRegisterHelper
{
    public class LoginRegisterHelper
    {
        public static string HashGen(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(password + "AutoWaresShop2025");
            var encodedByte = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedByte).Replace("-", "").ToLower();
        }
    }
}
