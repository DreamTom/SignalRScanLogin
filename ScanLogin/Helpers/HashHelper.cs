using System.Security.Cryptography;
using System.Text;

namespace ScanLogin.Helpers
{
    public class HashHelper
    {
        public static string GetMd5(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            byte[] bs = MD5.HashData(buffer);
            return BitConverter.ToString(bs).Replace("-", "");
        }
    }
}
