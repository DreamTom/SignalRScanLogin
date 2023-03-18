
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ScanLogin.Models;

namespace ScanLogin.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IHubContext<LoginHub> _hubContext;

        public AuthController(IMemoryCache memoryCache, IHubContext<LoginHub> hubContext)
        {
            _memoryCache = memoryCache;
            _hubContext = hubContext;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<string> Login([FromServices] IOptions<SignConfig> signConfigOption, LoginUserDto loginUser)
        {
            if (loginUser.UserName != "admin" || loginUser.Password != "123456") return BadRequest("用户名或密码错误");

            var signConfig = signConfigOption.Value;
            var user = new UserInfo()
            {
                UserId = loginUser.UserName,
                ExpireTime = DateTimeOffset.Now.AddMinutes(signConfig.Expire).ToUnixTimeMilliseconds()
            };
            return ECHelper.IssueToken(user, signConfig);
        }

        /// <summary>
        /// 使用token登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost("loginRemote")]
        public ActionResult LoginRemote()
        {
            return Ok($"用户{UserId}登陆成功");
        }

        /// <summary>
        /// 模拟生成二维码
        /// </summary>
        /// <returns></returns>
        [HttpGet("getQrCode")]
        public ActionResult<string> GetQrCode()
        {
            var clientIdentity = HashHelper.GetMd5("hello" + DateTimeOffset.Now.ToUnixTimeMilliseconds());
            return Ok(clientIdentity);
        }

        /// <summary>
        /// 扫描登陆
        /// </summary>
        /// <param name="clientId">客户端唯一标识</param>
        /// <returns></returns>
        [HttpGet("scanLogin")]
        public async Task<ActionResult> ScanLogin(string clientId)
        {
            var connId = _memoryCache.Get<string>(clientId);
            await _hubContext.Clients.Client(connId).SendAsync("GetScanState", (int)ScanState.HasScanned, Token);
            return Ok();
        }
    }
}
