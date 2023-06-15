namespace GR.Security
{
    /// <summary>
    /// 加密
    /// </summary>
    public sealed class Md5
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <returns></returns>
        public static string Encrypt(string txt)
        {
            return Md5Helper.Instance.Encrypt(txt);
        }
    }
}
