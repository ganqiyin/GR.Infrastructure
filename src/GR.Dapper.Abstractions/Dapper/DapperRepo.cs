using Dapper;
using System.Data;

namespace GR.Dapper
{
    public abstract class DapperRepo
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        protected readonly IRepoContext _dbContext;

        private readonly IDbConnection _dbConnection;

        public DapperRepo(IRepoContext dbContext)
        {
            _dbContext = dbContext;
            _dbConnection = dbContext.Connection;
        }

        public virtual IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public virtual async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public virtual async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryMultipleAsync(sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }
    }
}
