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
        public UserInfo DecryptToken(string token, SignConfig config)
        {
            var paylod = token.Split('_')[0];
            var realToken = HashHelper.GetMd5(paylod + config.SecretKey);
            if (realToken == token)
            {
                byte[] bs = Convert.FromBase64String(realToken);
                string text = Encoding.UTF8.GetString(bs);
                return JsonSerializer.Deserialize<UserInfo>(text);
            }
            else
                return null;
        }

        public string IssueToken(UserInfo userInfo, SignConfig config)
        {
            string text = JsonSerializer.Serialize(userInfo);
            var payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            var sign = HashHelper.GetMd5(payload + config.SecretKey);
            return payload + "_" + sign;
        }
    }
}
