namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>
        /// .xxx
        /// </returns>
        public static string GetFileExt(this string fileName)
        {
            return Path.GetExtension(fileName);
        }

        /// <summary>
        /// 数据流转字节
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this FileStream fileStream)
        {
            byte[] fileData = new byte[fileStream.Length];
            fileStream.Read(fileData, 0, fileData.Length);
            fileStream.Close();
            return fileData;
        }

        /// <summary>
        /// 数据流转字节
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            //设置当前流位置开始
            stream.Seek(0, SeekOrigin.Begin);
            return data;
        }

        /// <summary>
        /// 字节转数据流
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Stream ToStream(this byte[] data)
        {
            return new MemoryStream(data);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string fileName)
        {
            // 打开文件 
            return new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                   .ToBytes();
        }

        /// <summary>
        /// 字节转 base64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToBase64Str(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// base64 转字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string data)
        {
            return Convert.FromBase64String(data);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public static void ToSave(this byte[] data, string fileName)
        {
            var fs = new FileStream(fileName, FileMode.CreateNew);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
