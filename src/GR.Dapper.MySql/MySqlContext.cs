using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace GR.Dapper.MySql
{
    //public class MySqlContext : RepoContext
    //{
    //    private readonly string _connStr;

    //    public MySqlContext(IConfiguration configuration)
    //        : base()
    //    {
    //        var connStr = configuration.GetConnectionString("MySql");
    //        if (connStr == null)
    //            throw new ArgumentNullException("数据库连接未创建");
    //    }

    //    protected override void InitConnection()
    //    {
    //        base.Connection = new MySqlConnection(_connStr);
    //    }
    //}
}
