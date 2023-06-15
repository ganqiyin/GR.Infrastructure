namespace GR.Extensions
{
    /// <summary>
	/// 扩展
	/// </summary>
	public static partial class Extension
    {
        /// <summary>
        /// 转换 int64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static long ToInt64(this string input)
        {
            if (long.TryParse(input, out long val))
                return val;
            else
                return 0;
        }

        /// <summary>
        /// 转换 int64
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IEnumerable<long> ToInt64(this IEnumerable<string> input)
        {
            if (input.IsEmpty())
                return new List<long>();
            var result = new List<long>();
            foreach (var item in input)
            {
                var d = item.ToInt64();
                if (d <= 0)
                    continue;
                result.Add(d);
            }
            return result;
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <returns></returns>
        public static double CalculatePercentage(this int dividend, int divisor, int decimalDigits = 0)
        {
            return dividend.Divide(divisor).MultiplyBy(100).Round(decimalDigits);
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <returns></returns>
        public static double CalculatePercentage(this double dividend, double divisor, int decimalDigits = 0)
        {
            return dividend.Divide(divisor).MultiplyBy(100).Round(decimalDigits);
        }

        /// <summary>
        /// 计算百分比
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <returns></returns>
        public static decimal CalculatePercentage(this decimal dividend, decimal divisor, int decimalDigits = 0)
        {
            return dividend.Divide(divisor).MultiplyBy(100).Round(decimalDigits);
        }

        /// <summary>
        /// 被除数÷除数
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <returns></returns>
        public static double Divide(this int dividend, int divisor)
        {
            if (divisor == 0)
                return 0;
            return dividend / (double)divisor;
        }

        /// <summary>
        /// 被除数÷除数
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <returns></returns>
        public static decimal Divide(this decimal dividend, decimal divisor)
        {
            if (divisor == 0)
                return 0;
            return dividend / divisor;
        }

        /// <summary>
        /// 被除数÷除数
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="divisor">除数</param>
        /// <returns></returns>
        public static double Divide(this double dividend, double divisor)
        {
            if (divisor == 0)
                return 0;
            return dividend / divisor;
        }

        /// <summary>
        /// 乘数 * 乘数 
        /// </summary>
        /// <param name="x">乘数</param>
        /// <param name="y">乘数</param>
        /// <returns></returns>
        public static double MultiplyBy(this double x, double y)
        {
            return x * y;
        }

        /// <summary>
        /// 乘数 * 乘数 
        /// </summary>
        /// <param name="x">乘数</param>
        /// <param name="y">乘数</param>
        /// <returns></returns>
        public static decimal MultiplyBy(this decimal x, decimal y)
        {
            return x * y;
        }

        /// <summary>
        /// 舍入到指定小数位
        /// </summary>
        /// <param name="orgData"></param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <returns></returns>
        public static double Round(this double orgData, int decimalDigits)
        {
            return Math.Round(orgData, decimalDigits);
        }

        /// <summary>
        /// 舍入到指定小数位
        /// </summary>
        /// <param name="orgData"></param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <returns></returns>
        public static decimal Round(this decimal orgData, int decimalDigits)
        {
            return Math.Round(orgData, decimalDigits);
        }

        /// <summary>
        /// 舍入到指定小数位
        /// </summary>
        /// <param name="orgData"></param>
        /// <param name="decimalDigits">要保留的小数位数</param>
        /// <param name="mode">舍入方式</param>
        /// <returns></returns>
        public static double Round(this double orgData, int decimalDigits, MidpointRounding mode)
        {
            return Math.Round(orgData, decimalDigits, mode);
        }

        /// <summary>
        /// 舍入到指定小数位
        /// </summary>
        /// <param name="orgData"></param>
        /// <param name="decimalDigits">要保留的小数位数</param> 
        /// <returns></returns>
        public static double RoundFromZero(this double orgData, int decimalDigits)
        {
            return orgData.Round(decimalDigits, MidpointRounding.AwayFromZero);
        }
    }
}
