using System.Web.Mvc;
using WebShop.Domain.User.Admin;

namespace WebShop.Filters
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userRole = "Guest";
            if (SessionHelper.User is UserInfo userInfo && userInfo.Role != null)
            {
                userRole = userInfo.Role;
            }
            else filterContext.Result = new RedirectResult("~/Error/AccessDenied");

            if (userRole != "Admin")
            {
                filterContext.Result = new RedirectResult("~/Error/AccessDenied");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}