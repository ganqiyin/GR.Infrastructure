using System.Data;

namespace GR.Dapper
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        bool IsCommit { set; get; }

        IDbTransaction Tran { get; }

        void BeginTran();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

    }
}
