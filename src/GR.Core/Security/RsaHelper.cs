using System.Security.Cryptography;
using System.Text;

namespace GR.Security
{
    /// <summary>
    /// 标准的-公钥加密-私钥解密
    /// </summary>
    /// <remarks>
    /// 参考：https://www.cnblogs.com/runliuv/p/17467994.html
    /// 密钥小助手：https://opendocs.alipay.com/common/02kipk?pathHash=0d20b438
    /// </remarks>
    internal sealed class RsaHelper
    {
        /*不完全懒汉，但又不加锁的线程安全*/
        private static readonly RsaHelper _instance = new RsaHelper();
        static RsaHelper() { }

        private RsaHelper()
        {
        }

        /// <summary>
        /// 实列
        /// </summary>
        public static RsaHelper Instance { get { return _instance; } }

        /** 默认编码字符集 */
        public const string DEFAULT_CHARSET = "UTF-8";

        /// <summary>
        /// 公钥加密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <param name="charset">编码:UTF-8</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string RSAEncrypt(string content, string publicKeyPem, string charset = DEFAULT_CHARSET)
        {
            return RSAEncrypt(content, Encoding.GetEncoding(charset), publicKeyPem);
        }


        /// <summary>
        /// 公钥加密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">明文</param>
        /// <param name="encoding">编码</param>
        /// <param name="publicKeyPem">公钥</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string RSAEncrypt(string content, Encoding encoding, string publicKeyPem)
        {
            //假设私钥长度为1024， 1024/8-11=117。
            //如果明文的长度小于117，直接全加密，然后转base64。(data.Length <= maxBlockSize)
            //如果明文长度大于117，则每117分一段加密，写入到另一个Stream中，最后转base64。while (blockSize > 0)                 
            try
            {
                //转为纯字符串，不带格式
                publicKeyPem = publicKeyPem.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\r", "").Replace("\n", "").Trim();

                RSA rsa = RSA.Create();
                rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKeyPem), out _);

                byte[] data = encoding.GetBytes(content);
                int maxBlockSize = rsa.KeySize / 8 - 11; //加密块最大长度限制
                if (data.Length <= maxBlockSize)
                {
                    byte[] cipherbytes = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                    return Convert.ToBase64String(cipherbytes);
                }
                MemoryStream plaiStream = new MemoryStream(data);
                MemoryStream crypStream = new MemoryStream();
                byte[] buffer = new byte[maxBlockSize];
                int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                while (blockSize > 0)
                {
                    byte[] toEncrypt = new byte[blockSize];
                    Array.Copy(buffer, 0, toEncrypt, 0, blockSize);
                    byte[] cryptograph = rsa.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);
                    crypStream.Write(cryptograph, 0, cryptograph.Length);
                    blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                }

                return Convert.ToBase64String(crypStream.ToArray(), Base64FormattingOptions.None);
            }
            catch (Exception ex)
            {
                throw new Exception("EncryptContent = " + content + ",charset = " + encoding.ToString(), ex);
            }
        }


        /// <summary>
        /// 私钥解密（超过 私钥长度 / 8 - 11，分段加密）
        /// </summary>
        /// <param name="content">密文</param>
        /// <param name="privateKeyPem">私钥</param>
        /// <param name="keyFormat">私钥格式 PKCS1,PKCS8</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string RSADecrypt(string content, string privateKeyPem, string keyFormat, string charset = DEFAULT_CHARSET)
        {
            return RSADecrypt(content, Encoding.GetEncoding(charset), privateKeyPem, keyFormat);
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
        public string RSADecrypt(string content, Encoding encoding, string privateKeyPem, string keyFormat)
        {
            try
            {
                //假设私钥长度为1024， 1024/8 =128。
                //如果明文的长度小于 128，直接全解密。(data.Length <= maxBlockSize)
                //如果明文长度大于 128，则每 128 分一段解密，写入到另一个Stream中，最后 GetString。while (blockSize > 0)                                 

                //转为纯字符串，不带格式
                privateKeyPem = privateKeyPem.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\r", "").Replace("\n", "").Trim();
                privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Replace("\r", "").Replace("\n", "").Trim();


                RSA rsaCsp = RSA.Create();
                if (keyFormat == "PKCS8")
                    rsaCsp.ImportPkcs8PrivateKey(Convert.FromBase64String(privateKeyPem), out _);
                else if (keyFormat == "PKCS1")
                    rsaCsp.ImportRSAPrivateKey(Convert.FromBase64String(privateKeyPem), out _);
                else
                    throw new Exception("只支持PKCS8，PKCS1");

                byte[] data = Convert.FromBase64String(content);
                int maxBlockSize = rsaCsp.KeySize / 8; //解密块最大长度限制
                if (data.Length <= maxBlockSize)
                {
                    byte[] cipherbytes = rsaCsp.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                    return encoding.GetString(cipherbytes);
                }
                MemoryStream crypStream = new MemoryStream(data);
                MemoryStream plaiStream = new MemoryStream();
                byte[] buffer = new byte[maxBlockSize];
                int blockSize = crypStream.Read(buffer, 0, maxBlockSize);
                while (blockSize > 0)
                {
                    byte[] toDecrypt = new byte[blockSize];
                    Array.Copy(buffer, 0, toDecrypt, 0, blockSize);
                    byte[] cryptograph = rsaCsp.Decrypt(toDecrypt, RSAEncryptionPadding.Pkcs1);
                    plaiStream.Write(cryptograph, 0, cryptograph.Length);
                    blockSize = crypStream.Read(buffer, 0, maxBlockSize);
                }

                return encoding.GetString(plaiStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("DecryptContent = " + content + ",charset = " + encoding.ToString(), ex);
            }
        }
    }
}
