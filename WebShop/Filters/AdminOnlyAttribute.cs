using System.Web.Mvc;
using WebShop.Domain.User.Admin;

namespace WebShop.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            var userRole = "guest";
            if (session["User"] is UserInfo userInfo && userInfo.Role != null)
            {
                userRole = userInfo.Role;
            }
            //var userRole = (session["User"] as UserInfo).Role ;

            if (userRole != "Admin")
            {
                // редирект на страницу ошибки или отказа
                filterContext.Result = new RedirectResult("~/Error/AccessDenied");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}