using System.Data;

namespace GR.Dapper
{
    /// <summary>
    /// 仓储上下文
    /// </summary>
    public interface IRepoContext : IDisposable
    {
        /// <summary>
        /// DbConnection 
        /// </summary>
        IDbConnection Connection { get; }
    }
}
