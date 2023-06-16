using Autofac;
using GR.Autofac;
using GR.EfCore.Tests.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GR.EfCore.Tests
{
    public class EfCoreFixture : IDisposable
    {
        public IHost _Host { get; }

        public EfCoreFixture()
        {
            _Host = GetHost();
        }

        private IHost GetHost()
        {
            IHost host = Host.CreateDefaultBuilder()
                             .ConfigureServices((builder, services) =>
                             {
                                 //services.AddMySql<TestDbContext>(builder.Configuration);
                                 services.AddMySql<TestDbContext>(Init());
                             })
                             .ConfigureContainer<ContainerBuilder>(containerBuilder =>
                             {
                                 containerBuilder.AddServices(typeof(EfCoreFixture).Assembly);
                             })
                             .UseAutofac()
                            .Build();

            return host;
        }

        private IConfiguration Init()
        {
            var config = new ConfigurationBuilder()
                       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                       .AddJsonFile("appsettings.json", true, true)
                       //.AddJsonFile("appsettings.Development.json", true, true)
                       .Build();
            return config;
        }

        public void Dispose()
        {

        }
    }
}