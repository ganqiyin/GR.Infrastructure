using System.Text;

namespace GR.Security
{
    /// <summary>
    /// RSA-加解密
    /// </summary>
    public sealed class Rsa
    {
        /// <summary>
        /// 公钥加密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <param name="charset">编码:UTF-8</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string Encrypt(string content, string publicKeyPem, string charset = RsaHelper.DEFAULT_CHARSET)
        {
            if (string.IsNullOrEmpty(charset))
            {
                charset = RsaHelper.DEFAULT_CHARSET;
            }
            return RsaHelper.Instance.RSAEncrypt(content, publicKeyPem, charset: charset);
        }

        /// <summary>
        /// 公钥加密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="encoding">编码</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string Encrypt(string content, Encoding encoding, string publicKeyPem)
        {
            return RsaHelper.Instance.RSAEncrypt(content, encoding, publicKeyPem);
        }

        /// <summary>
        /// 私钥解密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="privateKeyPem">私钥</param>
        /// <param name="keyFormat">私钥格式 PKCS1,PKCS8</param>
        /// <param name="charset">编码:UTF-8</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string Decrypt(string content, string privateKeyPem, string keyFormat, string charset = RsaHelper.DEFAULT_CHARSET)
        {
            if (string.IsNullOrEmpty(charset))
            {
                charset = RsaHelper.DEFAULT_CHARSET;
            }
            return RsaHelper.Instance.RSADecrypt(content, privateKeyPem, keyFormat, charset: charset);
        }

        /// <summary>
        /// 私钥解密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="encoding">编码</param>
        /// <param name="privateKeyPem">私钥</param>
        /// <param name="keyFormat">私钥格式 PKCS1,PKCS8</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string Decrypt(string content, Encoding encoding, string privateKeyPem, string keyFormat)
        {
            return RsaHelper.Instance.RSADecrypt(content, encoding, privateKeyPem, keyFormat);
        }

    }
}
