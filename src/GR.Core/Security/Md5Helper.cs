using System.Text;

namespace GR.Security
{
    /// <summary>
    /// MD5加密
    /// </summary>
    internal sealed class Md5Helper
    {
        /*不完全懒汉，但又不加锁的线程安全*/
        private static readonly Md5Helper _instance = new Md5Helper();
        static Md5Helper() { }

        private Md5Helper()
        {
        }

        /// <summary>
        /// 实列
        /// </summary>
        public static Md5Helper Instance { get { return _instance; } }

        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Encrypt(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var txt = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    txt.Append(data[i].ToString("x2"));
                }
                return txt.ToString();
            }
        }
    }
}
