using System.Net;

namespace GR.Dto
{
    /// <summary>
    /// 返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result, IResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        protected Result()
            : base() { }

        /// <summary>
        /// 构造函数
        /// 默认赋值 code=200
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        protected Result(T data, string msg = "OK")
            : this(HttpStatusCode.OK.ToCode(), data, msg: msg)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">
        /// 消息码
        /// </param>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        protected Result(int code, T data, string msg = "")
            : base(code, msg: msg)
        {
            Data = data;
        }

        public T Data { get; private set; }

        /// <summary>
        /// 创建成功返回结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResult<T> OK(T data, string msg = "")
        {
            return new Result<T>(data, msg);
        }

        /// <summary>
        /// 创建错误返回结果
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResult<T> Error(T data, int code, string msg = "")
        {
            return new Result<T>(code, data, msg);
        }

        /// <summary>
        /// 创建错误返回结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static new IResult<T> Error(string msg = "")
        {
            return Error(default, HttpStatusCode.BadRequest.ToCode(), msg);
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result : IResult
    {
        protected Result()
          : this(msg: HttpStatusCode.OK.ToString())
        {

        }
        /// <summary>
        /// 构造函数
        /// 默认赋值 code=200
        /// </summary>
        /// <param name="msg"></param>
        protected Result(string msg = "OK")
            : this(HttpStatusCode.OK.ToCode(), msg: msg)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">
        /// 消息码
        /// </param>
        /// <param name="msg"></param>
        protected Result(int code, string msg = "")
        {
            Code = code;
            Msg = msg;
        }

        public int Code { get; private set; }

        public string Msg { get; private set; }

        public bool Success
        {
            get
            {
                return Code == (int)HttpStatusCode.OK;
            }
        }

        /// <summary>
        /// 创建成功返回结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResult OK(string msg = "")
        {
            return new Result(msg);
        }

        /// <summary>
        /// 创建错误返回结果
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResult Error(int code, string msg = "")
        {
            return new Result(code, msg);
        }

        /// <summary>
        /// 创建错误返回结果
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static IResult Error(string msg = "")
        {
            return Error(HttpStatusCode.BadRequest.ToCode(), msg);
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转code
        /// </summary>
        /// <param name="httpStatus"></param>
        /// <returns></returns>
        public static int ToCode(this HttpStatusCode httpStatus)
        {
            return (int)httpStatus;
        }

        /// <summary>
        /// 转枚举
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static HttpStatusCode ToStatusCode(this int code)
        {
            return (HttpStatusCode)code;
        }
    }
}
