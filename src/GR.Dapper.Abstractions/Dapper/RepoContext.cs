using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;

namespace GR.Dapper
{
    public class RepoContext : DisposableObject, IUnitOfWork, IRepoContext
    {
        protected readonly DapperOption _dapperOption;
        public RepoContext(IOptions<DapperOption> options)
        {
            _dapperOption = options.Value;
            //
            initConnection();
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        /// <remarks>
        /// 参考文档：
        /// https://learn.microsoft.com/zh-cn/dotnet/framework/data/adonet/obtaining-a-dbproviderfactory
        /// https://blog.csdn.net/liynet/article/details/129064229
        /// https://blog.csdn.net/sD7O95O/article/details/101727966
        /// https://www.cnblogs.com/tuousi99/p/4455573.html
        /// </remarks>
        protected virtual void initConnection()
        {
            if (_dapperOption == null)
                throw new ArgumentNullException("数据库链接字符串未配置");
            var factory = DbProviderFactories.GetFactory(_dapperOption.ProviderName);
            _dbConnection = factory.CreateConnection();
            if (_dbConnection == null)
                throw new ArgumentNullException("数据库链接创建失败");
            //
            _dbConnection.ConnectionString = _dapperOption.ConnStr;
        }

        protected IDbConnection _dbConnection;

        public IDbConnection Connection
        {
            get
            {
                if (_dbConnection == null)
                    initConnection();
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();
                return _dbConnection;
            }
        }

        private bool _isCommit = true;

        private readonly object _sync = new object();

        public bool IsCommit
        {
            get
            {
                return _isCommit;
            }
            set
            {
                _isCommit = value;
            }
        }

        public IDbTransaction Tran { get; private set; }

        public void BeginTran()
        {
            this.Tran = this.Connection.BeginTransaction();
            this.IsCommit = false;
        }

        public void Commit()
        {
            if (this.IsCommit)
            {
                return;
            }
            lock (_sync)
            {
                this.Tran.Commit();
                this._isCommit = true;
            }
        }

        public void Rollback()
        {
            if (this.IsCommit)
            {
                return;
            }
            lock (_sync)
            {
                this.Tran.Rollback();
                this._isCommit = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            if (this.Connection.State != ConnectionState.Open)
            {
                return;
            }
            this.Commit();
            //先关闭，后释放
            this.Connection.Close();
            this.Connection.Dispose();
        }
    }
}
