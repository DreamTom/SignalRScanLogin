using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ScanLogin.Filters
{
    public class AuthFilter : IAsyncAuthorizationFilter
    {
        private readonly SignConfig _signConfig;

        public AuthFilter(IOptions<SignConfig> signConfigOption)
        {
            _signConfig = signConfigOption.Value;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var isIgnoreAuth = context.ActionDescriptor.EndpointMetadata.Any(
                q => q.GetType() == typeof(AllowAnonymousAttribute));
            if(isIgnoreAuth) return Task.CompletedTask;

            var hasToken = context.HttpContext.Request.Headers.TryGetValue("token", out var token);
            if (hasToken)
            {
                try
                {
                    ECHelper.DecryptToken(token, _signConfig);
                    return Task.CompletedTask;
                }
                catch (Exception e)
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Result = new ObjectResult(e.Message);
                }
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new ObjectResult("未找到token信息");
            }
            return Task.CompletedTask;
        }
    }
}
