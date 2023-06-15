using GR.Security;
using System.Text;

namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 不为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotEmpty(this string input)
        {
            return !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// 为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string input)
        {
            return !input.IsNotEmpty();
        }

        /// <summary>
        /// 混淆数据
        /// </summary>
        /// <param name="input"></param>
        /// <param name="saltData">用来混淆的数据</param>
        /// <returns></returns>
        public static string ToMix(this string input, string saltData)
        {
            if (saltData.IsEmpty())
                return input;
            var mixArr = saltData.ToCharArray();
            var inputArr = input.ToCharArray();
            if (saltData.Length > input.Length)
            {
                return mixArr.ToMix(inputArr);
            }
            else
            {
                return inputArr.ToMix(mixArr);
            }
        }

        /// <summary>
        /// 混淆数据
        /// </summary>
        /// <param name="sourceArr"></param>
        /// <param name="saltArr"></param>
        /// <returns></returns>
        public static string ToMix(this char[] sourceArr, char[] saltArr)
        {
            var txt = new StringBuilder();
            for (var i = 0; i < sourceArr.Length; i++)
            {
                txt.Append(sourceArr[i]);
                if (i < saltArr.Length)
                {
                    txt.Append(saltArr[i]);
                }
            }
            return txt.ToString();
        }

        /// <summary>
        /// 附加字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Append(this string input, string data)
        {
            return string.Format("{0}{1}", data, input);
        }

        /// <summary>
        /// 附加字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Append(this string input, long data)
        {
            return string.Format("{0}{1}", data, input);
        }

        /// <summary>
        /// 附加字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Append(this string input, int data)
        {
            return string.Format("{0}{1}", data, input);
        }

        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMd5Encrypt(this string input)
        {
            if (input.IsEmpty())
                return "";
            var salt = "$%^@&*`#";
            return input.ToMd5Encrypt(salt);
        }

        /// <summary>
        /// md5 加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string ToMd5Encrypt(this string input, string salt)
        {
            if (input.IsEmpty())
                return "";
            return Md5Helper.Instance.Encrypt(input.ToMix(salt));
        }
    }
}
