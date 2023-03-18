using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ScanLogin.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string UserId
        {
            get
            {
                var signOption = HttpContext.RequestServices.GetRequiredService<IOptions<SignConfig>>();
                return ECHelper.DecryptToken(Token,signOption.Value).UserId;
            }
        }

        protected string Token
        {
            get
            {
                var res = HttpContext.Request.Headers.TryGetValue("token", out var token);
                if (!res) throw new ArgumentException("token不能为空");
                return token;
            }
        }
    }
}
