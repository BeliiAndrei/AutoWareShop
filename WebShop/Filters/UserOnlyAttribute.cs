using System.Web.Mvc;
using WebShop.Domain.User.Admin;

namespace WebShop.Filter
{
    //This filter is used to give access only to authorised and not banned users of WebShop application.
    public class UserOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var userRole = "Guest";
            if (session["User"] is UserInfo userInfo && userInfo.Role != null && userInfo.Role != "Banned")
            {
                userRole = userInfo.Role;
            }

            if (userRole != "User" && userRole != "Admin")
            {
                filterContext.Result = new RedirectResult("~/Auth/Authorisation");
            }
            

            base.OnActionExecuting(filterContext);
        }
    }
}