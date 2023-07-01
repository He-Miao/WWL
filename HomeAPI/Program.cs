using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Converters;
using Common.Helpers;
using HomeAPI.Filters;
using Microsoft.OpenApi.Models;
using Serilog;
using SqlSugar;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);
var basePath = AppContext.BaseDirectory;

//���������ļ�
var _config = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();
builder.Services.AddSingleton(new AppSettingsHelper(_config));

#region ע�����ݿ�
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

#region ��ʼ����־
//Serilog:https://igeekfan.gitee.io/vovo-docs/dotnetcore/examples/serilog-tutorial.html
builder.Host.UseSerilog((builderContext, config) =>
{
    config
    .MinimumLevel.Warning()
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine("Logs", @"Log.txt"), rollingInterval: RollingInterval.Day);
});
#endregion

#region ���swaggerע��
if (AppSettingsHelper.Get("UseSwagger").ToBool())
{
    builder.Services.AddSwaggerGen(a =>
    {
        a.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Api",
            Description = "Api�ӿ��ĵ�"
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



#region ��ʼ��Autofac ע����򼯣�ʹ��Autofac�滻.Net����Ĭ�ϵ�DI������
//ʹ��AutofacServiceProviderFactory��Ϊ�����ķ����ṩ�߹�����������ASP.NET CoreӦ�ó���Ķ������������������Ӧ�ó��򲢹�������ע������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
var hostBuilder = builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    //��ȡ��ǰִ�еĳ���
    var assembly = Assembly.GetExecutingAssembly();
    //ʹ��builder����ע�������������"Repository"��β�����͡�ע��ķ�ʽ�����䱾��ʵ�ֵĽӿ����ͣ�AsImplementedInterfaces��
    builder.RegisterAssemblyTypes(assembly).Where(a=>a.Name.EndsWith("Repository")).AsImplementedInterfaces();
    //ʹ��builder����ע�������������"Service"��β�����͡�ע��ķ�ʽ�����䱾��ʵ�ֵĽӿ�����
    builder.RegisterAssemblyTypes(assembly).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces();
});
#endregion

// ע�������
builder.Services.AddControllersWithViews(options =>
{
    //��GlobalExceptionFilter��������ӵ�������ѡ����
    options.Filters.Add<GlobalExceptionFilter>();  
});


var app = builder.Build();

//����·���м��������������������URL��������ָ����·�ɹ�������·�ɵ���Ӧ�Ĵ��������������ͨ��ʹ��·���м�������Զ��岻ͬ�Ķ˵��·�ɹ����Ա㽫�����͵���ȷ�Ĵ��������������
app.UseRouting();
//���������֤�м��
app.UseAuthentication();
//������Ȩ�м�����������û�����Դ�ķ���Ȩ��
app.UseAuthorization();

#region ����swaggerUI
if (AppSettingsHelper.Get("UseSwagger").ToBool())
{
    app.UseSwagger();
    app.UseSwaggerUI(a =>
    {
        a.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
        a.DocExpansion(DocExpansion.None);
        a.DefaultModelsExpandDepth(-1);//����ʾModels
    });
}
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
