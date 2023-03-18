using ScanLogin.Configs;
using ScanLogin.Models;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;

namespace ScanLogin.Helpers
{
    /*
     * 自定义加密规则  (payload)加密个人信息+(sign)签名，中间用下划线'_'分开
     * payload = MD5(用户信息+过期时间) sign = MD5(私钥+payload)
     */

    public class ECHelper
    {
        public static UserInfo DecryptToken(string token, SignConfig config)
        {
            var payloads = token.Split('.');

            if (payloads.Length != 2)
                throw new ArgumentException("token不合法");

            var realSign = HashHelper.GetMd5(payloads[0] + config.SecretKey);
            var sign = payloads[1];
            if (realSign != sign) throw new ArgumentException("token不合法");

            var bs = Convert.FromBase64String(payloads[0]);
            var text = Encoding.UTF8.GetString(bs);
            var user = JsonSerializer.Deserialize<UserInfo>(text);
            var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            if (user.ExpireTime <= timestamp)
                throw new ArgumentException("token已过期");
            return user;
        }

        public static string IssueToken(UserInfo userInfo, SignConfig config)
        {
            var text = JsonSerializer.Serialize(userInfo);
            var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            var sign = HashHelper.GetMd5(payload + config.SecretKey);
            return payload + "." + sign;
        }
    }
}
