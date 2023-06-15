using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Net;

namespace GR.Swagger
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class NSwagExtension
    {
        /// <summary>
        /// 注入 Swagger 服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title">文档标题</param>
        /// <param name="description">文档描述</param>
        /// <param name="contact"></param>
        /// <param name="license"></param>
        /// <returns></returns>
        public static IServiceCollection AddNSwagger(this IServiceCollection services, string title = "", string description = "", OpenApiContact contact = null, OpenApiLicense license = null)
        {
            services.AddApiVersioning(option =>
            {
                // 可选，为true时API返回支持的版本信息
                option.ReportApiVersions = true;
                // 不提供版本时，默认为1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                //版本信息放到header ,不写在不配置路由的情况下，版本信息放到response url 中
                //option.ApiVersionReader = new HeaderApiVersionReader("api-version");
                // 请求中未指定版本时默认为1.0
                option.DefaultApiVersion = new ApiVersion(1, 0);
            }).AddVersionedApiExplorer(option =>
            {   // 版本名的格式：v+版本号
                option.GroupNameFormat = "'v'V";
                option.AssumeDefaultVersionWhenUnspecified = true;
            });

            ////获取webapi版本信息，用于swagger多版本支持 
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var apiVerDesc in provider.ApiVersionDescriptions)
            {
                services.AddSwaggerDocument(document =>
                {
                    document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));
                    document.DocumentName = apiVerDesc.GroupName;
                    document.Version = apiVerDesc.GroupName;
                    document.Title = title;
                    document.Description = description;
                    //如果相同版本信息路由下增加   [ApiExplorerSettings(GroupName = "v1")] 进行区分即可
                    document.ApiGroupNames = new string[] { apiVerDesc.GroupName };
                    //可以设置从注释文件加载，但是加载的内容可被 OpenApiTagAttribute 特性覆盖
                    document.UseControllerSummaryAsTagDescription = true;
                    document.PostProcess = process =>
                    {
                        process.Info.Contact = contact ?? new OpenApiContact
                        {
                            Name = "联系人",
                            Email = "example@xx.com",
                            Url = "https://example.com"
                        };
                        process.Info.License = license ?? new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = "https://example.com/license"
                        };
                    };
                    //jwt 认证
                    document.AddSecurity("JWT token", Enumerable.Empty<string>(),
                          new OpenApiSecurityScheme()
                          {
                              Type = OpenApiSecuritySchemeType.ApiKey,
                              Name = nameof(Authorization),
                              In = OpenApiSecurityApiKeyLocation.Header,
                              Description = "将token值复制到如下格式: \nBearer {token}"
                          }
                      );

                });
            }

            //services.AddSwaggerDocument(settings =>
            //{
            //    settings.DocumentName = "v1";
            //    settings.Version = "v0.0.1";
            //    settings.Title = title;
            //    //可以设置从注释文件加载，但是加载的内容可被 OpenApiTagAttribute 特性覆盖
            //    settings.UseControllerSummaryAsTagDescription = true;

            //    //定义JwtBearer认证方式一
            //    settings.AddSecurity("JwtBearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme()
            //    {
            //        Description = "这是方式一(直接在输入框中输入认证信息，不需要在开头添加Bearer)",
            //        Name = "Authorization",//jwt默认的参数名称
            //        In = OpenApiSecurityApiKeyLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
            //        Type = OpenApiSecuritySchemeType.Http,
            //        Scheme = "bearer"
            //    });
            //});
            return services;
        }

        /// <summary>
        /// 注册 Swagger 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseNSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}