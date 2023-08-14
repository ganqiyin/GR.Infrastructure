using Serilog;

namespace GR.Logger
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class SerilogExtension
    {
        /// <summary>
        /// 创建日志提供器
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="serilogOutputTemplate"></param>
        /// <returns></returns>
        public static Serilog.Core.Logger InitLogger(this LoggerConfiguration loggerConfig, string serilogOutputTemplate = "")
        {
                serilogOutputTemplate = string.IsNullOrEmpty(serilogOutputTemplate) ? "{NewLine}{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 100) : serilogOutputTemplate;

                var logPath = Path.Combine(AppContext.BaseDirectory, "Logs", "log_.log");

                loggerConfig = loggerConfig
#if DEBUG
                    .MinimumLevel.Debug() // 所有Sink的最小记录级别
#else
                    .MinimumLevel.Warning()
#endif
                    //如果遇到 Microsoft 命名空间，日志最小级别为 info
                    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
                    .Enrich.FromLogContext() //记录相关上下文信息
                    .Enrich.WithProperty("WithMachineName", Environment.MachineName)
                    .Enrich.WithProperty("WithProcessId", Environment.ProcessId)
                    .Enrich.WithProperty("WithThreadId", Environment.CurrentManagedThreadId)
                    .WriteTo.Console()    //输出到控制台
                                          //.WriteTo.File(logPath, 
                                          //              rollingInterval: RollingInterval.Day,
                                          //              outputTemplate: SerilogOutputTemplate
                                          //              , retainedFileCountLimit: 31
                                          //              )
                     .WriteTo.Async(w => w.File(logPath,
                                  rollingInterval: RollingInterval.Day,
                                  outputTemplate: serilogOutputTemplate
                                  , retainedFileCountLimit: 31
                                  )
                     );
            return loggerConfig.CreateLogger();
        }
    }
}
