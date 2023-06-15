using System.Runtime.InteropServices;

namespace GR.Utils
{
    /// <summary>
    /// 系统工具
    /// </summary>
    public class OSSysUtil
    {
        /// <summary>
        /// 是否 linux
        /// </summary>
        /// <returns></returns>
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        /// <summary>
        /// 是否FreeBSD
        /// </summary>
        /// <returns></returns>
        public static bool IsFreeBSD()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);
        }

        /// <summary>
        /// 是否 OSX
        /// </summary>
        /// <returns></returns>
        public static bool IsOSX()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }

        /// <summary>
        /// 是否 Windows
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

    }
}
