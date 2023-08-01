namespace GR.Dto
{
    /// <summary>
    /// 返回结果接口约定
    /// </summary>
    /// <typeparam name="T">
    /// 返回数据类型
    /// </typeparam>
    public interface IResult<T> : IResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        T Data { get; }
    }

    /// <summary>
    /// 返回结果接口约定
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// 消息码 HttpStatusCode
        /// </summary>
        int Code { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        string Msg { get; }

        /// <summary>
        /// 是否成功消息
        /// </summary>
        bool Success { get; }
    }
}
