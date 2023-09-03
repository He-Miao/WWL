using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Converters;
using Common.Helpers;
using HomeAPI.AutoMappers;
using HomeAPI.Model;
using HomeAPI.Model.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;
using SqlSugar;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var basePath = AppContext.BaseDirectory;

//引入配置文件
var _config = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();
builder.Services.AddSingleton(new AppSettingsHelper(_config));

#region 配置跨域

builder.Services.AddCors(options =>
{
    options.AddPolicy
    (name: "myHomeAPI",
        builde =>
        {
            builde.WithOrigins("*", "*", "*")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

#endregion

#region 注入数据库
var dbtype = DbType.SqlServer;
if (AppSettingsHelper.Get("SugarConnectDBType", true) == "SqlServer")
{
    dbtype = DbType.SqlServer;
}
builder.Services.AddSingleton(options =>
{
    var sqlScope = new SqlSugarScope(new List<ConnectionConfig>()
    {
  new ConnectionConfig() { ConfigId = "default", ConnectionString = AppSettingsHelper.Get("SugarConnectString", true), DbType = dbtype, IsAutoCloseConnection = true }
    });
    return sqlScope;
});
#endregion

#region 初始化Serilog日志
//Serilog:https://igeekfan.gitee.io/vovo-docs/dotnetcore/examples/serilog-tutorial.html
builder.Host.UseSerilog((builderContext, config) =>
{
    config
    .MinimumLevel.Warning()
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine("Logs", @"Log.txt"), rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj} {Exception}{NewLine}");
});
#endregion

#region 添加swagger注释
if (AppSettingsHelper.Get("UseSwagger").ToBool())
{
    builder.Services.AddSwaggerGen(a =>
    {
        a.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Api",
            Description = "Api接口文档"
        });
        a.IncludeXmlComments(Path.Combine(basePath, "HomeAPI.xml"), true);
        a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Value: Bearer {token}",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        a.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }, Scheme = "oauth2", Name = "Bearer", In = ParameterLocation.Header }, new List<string>()
            }
        });
    });
}
#endregion

#region 初始化Autofac 注入程序集（使用Autofac替换.Net本身默认的DI容器）
//使用AutofacServiceProviderFactory作为主机的服务提供者工厂。主机是ASP.NET Core应用程序的顶级组件，它负责启动应用程序并管理依赖注入容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var hostBuilder = builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //获取当前执行的程序集
    var assembly = Assembly.GetExecutingAssembly();
    //使用builder对象注册程序集中所有以"Repository"结尾的类型。注册的方式是以其本身实现的接口类型（AsImplementedInterfaces）
    builder.RegisterAssemblyTypes(assembly).Where(a=>a.Name.EndsWith("Repository")).AsImplementedInterfaces();
    //使用builder对象注册程序集中所有以"Service"结尾的类型。注册的方式是以其本身实现的接口类型
    builder.RegisterAssemblyTypes(assembly).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
});
#endregion

#region 初始化AutoMapper 自动映射
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));
#endregion

#region 注册控制器
builder.Services.AddControllers(options =>
{
    //将GlobalExceptionFilter过滤器添加到控制器选项中
  //  options.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(options =>
{
    //通过配置 JsonSerializerOptions 来自定义 JSON 的日期格式
    //注意：这是使用 .NET 6 中的 System.Text.Json 实现自定义日期格式的方法。如果希望使用 Newtonsoft.Json，可以在 AddJsonOptions 中使用 NewtonsoftJson 方法替代 JsonSerializerOptions。
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
});
#endregion

var app = builder.Build();

#region 全局异常处理
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        //获取当前异常实例
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        //判断异常是否为空
        var exception = exceptionHandler?.Error;
        // 记录异常日志
        Log.Error(exception, "发生未处理的异常");
        // 返回自定义错误响应
        context.Response.StatusCode = 500;
        var responseData = new ResultData
        {
            Code = ResultCode.Error,
            Message = "发生了错误，请稍后再试！",
            Data = new object[] { }
        };
        var jsonString = JsonSerializer.Serialize(responseData);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonString);
    });
});
#endregion

app.UseCors("myHomeAPI");
//配置访问静态资源的路径（官方文档：https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0）
app.UseStaticFiles(new StaticFileOptions
{
    //资源所在的绝对路径
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    //表示访问路径,必须'/'开头
    RequestPath = "/uploads"
});
//启用路由中间件，负责解析传入请求的URL，并根据指定的路由规则将请求路由到相应的处理程序或控制器。通过使用路由中间件，可以定义不同的端点和路由规则，以便将请求发送到正确的处理程序或控制器。
app.UseRouting();
//启用身份验证中间件
app.UseAuthentication();
//启用授权中间件，负责处理用户对资源的访问权限
app.UseAuthorization();

#region 启用swaggerUI http://wwl.homeapi.com:5108/swagger/index.html
if (AppSettingsHelper.Get("UseSwagger").ToBool())
{
    app.UseSwagger();
    app.UseSwaggerUI(a =>
    {
        a.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
        a.DocExpansion(DocExpansion.None);
        a.DefaultModelsExpandDepth(-1);//不显示Models
    });
}
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
