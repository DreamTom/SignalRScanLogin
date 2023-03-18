namespace ScanLogin.Configs
{
    public class SignConfig
    {
        public string SecretKey { get; set; }

        /// <summary>
        /// 有效期：分钟
        /// </summary>
        public int Expire { get; set; }
    }
}
