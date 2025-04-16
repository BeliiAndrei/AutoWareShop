using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Domain.User.Admin;

namespace WebShop.Filter
{
    public class UserOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var userRole = "guest";
            if (session["User"] is UserInfo userInfo && userInfo.Role != null && userInfo.Role != "Banned")
            {
                userRole = userInfo.Role;
            }
            //var userRole = (session["User"] as UserInfo).Role ;

            if (userRole != "User" && userRole != "Admin")
            {
                // редирект на страницу ошибки или отказа
                filterContext.Result = new RedirectResult("~/Auth/Authorisation");
            }
            

            base.OnActionExecuting(filterContext);
        }
    }
}