using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mom_Managment.filter
{
    public class CheckAccess
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();

            // ✅ Skip if [AllowAnonymous]
            if (allowAnonymous != null)
                return;

            var userName = context.HttpContext.Session.GetString("UserName");

            // ✅ Check session
            if (string.IsNullOrEmpty(userName))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
        }

    }
}
