namespace GR.Extensions
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this string dateTime)
        {
            if (DateTimeOffset.TryParse(dateTime, out DateTimeOffset dt))
            {
                return dt;
            }
            return DateTimeOffset.MinValue;
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimeSeconds(this string dateTime)
        {
            if (dateTime.IsEmpty() || !DateTimeOffset.TryParse(dateTime, out DateTimeOffset dt))
            {
                return 0L;
            }
            return dt.ToUnixTimeSeconds();
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimeMilliseconds(this string dateTime)
        {
            if (dateTime.IsEmpty() || DateTimeOffset.TryParse(dateTime, out DateTimeOffset dt))
            {
                return 0L;
            }
            return dt.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ToLocalDateTime(this long dateTime)
        {
            return dateTime.ToDateTimeOffset().LocalDateTime;
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ToDateTimeOffset(this long dateTime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(dateTime);
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateFormat">时间格式，默认yyyy-MM-dd</param>
        /// <returns></returns>
        public static string FormatDateTime(this DateTime dateTime, string dateFormat = "yyyy-MM-dd")
        {
            return dateTime.ToString(dateFormat);
        }

        /// <summary>
        /// 时间转换(本地时间)
        /// </summary>
        /// <param name="unixTimeSeconds">时间戳，秒</param>
        /// <param name="dateFormat">时间格式</param>
        /// <returns></returns>
        public static string FormatDateTime(this long unixTimeSeconds, string dateFormat)
        {
            if (unixTimeSeconds <= 0)
            {
                return "";
            }
            return unixTimeSeconds.ToLocalDateTime().ToString(dateFormat);
        }

        /// <summary>
        /// 时间转换(本地时间)，默认yyyy-MM-dd
        /// </summary>
        /// <param name="unixTimeSeconds">时间戳，秒</param>
        /// <returns></returns>
        public static string FormatDate(this long unixTimeSeconds)
        {
            return unixTimeSeconds.FormatDateTime("yyyy-MM-dd");
        }

        /// <summary>
        /// 时间转换(本地时间)，默认yyyy-MM-dd HH:mm:ss (24 小时制)
        /// </summary>
        /// <param name="unixTimeSeconds">时间戳，秒</param>
        /// <returns></returns>
        public static string FormatDateTime(this long unixTimeSeconds)
        {
            return unixTimeSeconds.FormatDateTime("yyyy-MM-dd HH:mm:ss");
        }
    }
}
