using System.Security.Cryptography;
using System.Text;

namespace ScanLogin.Helpers
{
    public class HashHelper
    {
        public static string GetMd5(string text)
        {
            var buffer = Encoding.UTF8.GetBytes(text);
            var bs = MD5.HashData(buffer);
            return BitConverter.ToString(bs).Replace("-", "").ToLower();
        }
    }
}
