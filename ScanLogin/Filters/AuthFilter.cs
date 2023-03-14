using Microsoft.AspNetCore.Mvc.Filters;

namespace ScanLogin.Filters
{
    public class AuthFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool hasToken = context.HttpContext.Request.Headers.TryGetValue("token", out var token);
            if (hasToken)
            {

            }
        }
    }
}
