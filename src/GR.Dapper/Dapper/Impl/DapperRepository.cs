using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace GR.Dapper.Impl
{
    public class DapperRepository : IDapperRepository
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="type"><see cref="DapperDBType"/></param>
        public DapperRepository(string connStr, DapperDBType type)
        {
            switch (type)
            {
                case DapperDBType.Sqlite:
                    _dbConnection = new SqliteConnection(connStr);
                    break;
                case DapperDBType.Postgre:
                    _dbConnection = new NpgsqlConnection(connStr);
                    break;
                case DapperDBType.SqlServer:
                    _dbConnection = new SqlConnection(connStr);
                    break;
                case DapperDBType.Oracle:
                    _dbConnection = new OracleConnection(connStr);
                    break;
                case DapperDBType.MySql:
                    _dbConnection = new MySqlConnection(connStr);
                    break;
            }
            if (_dbConnection == null)
                throw new ArgumentNullException("数据库连接未创建");
        }

        public IDbConnection GetConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            return _dbConnection;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryMultipleAsync(sql, param: param, transaction: transaction, commandTimeout: commandTimeout, commandType: commandType);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            //_dbConnection?.Dispose();
        }

    }
}
