using Microsoft.Extensions.Logging;
using Nacos.V2;

namespace GR.Nacos.Handlers
{
    //public class NacosDiscoveryDelegatingHandler : DelegatingHandler
    //{
    //    private readonly ILogger _logger;
    //    private readonly INacosNamingService _nameSrv;

    //    public NacosDiscoveryDelegatingHandler(ILogger<NacosDiscoveryDelegatingHandler> logger
    //        , INacosNamingService nameSrv)
    //    {
    //        _logger = logger;
    //        _nameSrv = nameSrv;
    //    }

    //    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        //https://blog.csdn.net/wuchsh123/article/details/124921566
    //        //var currentRequest = request.RequestUri;
    //        //try
    //        //{
    //        //    var instance = await _nameSrv.SelectOneHealthyInstance(currentRequest.Host);
    //        //    //var host=string.Format("{}")
    //        //    //request.RequestUri=new Uri(string.Format("{0}{1}",instance.ServiceName))
    //        //}
    //        //catch (Exception ex)
    //        //{

    //        //}
    //        //finally
    //        //{

    //        //}
    //        return base.SendAsync(request, cancellationToken);
    //    }
    //}
}
