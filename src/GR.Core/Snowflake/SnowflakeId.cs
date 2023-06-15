namespace GR.Snowflake
{
    /// <summary>
    /// ID 生成器
    /// </summary>
    public sealed class SnowflakeId
    {
        /// <summary>
        /// 创建ID
        /// </summary>
        /// <returns></returns>
        public static long Create()
        {
            return SnowflakeIdGenerator.Instance.GenerateId();
        }
    }
}
