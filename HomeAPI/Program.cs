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

//���������ļ�
var _config = new ConfigurationBuilder()
                 .SetBasePath(basePath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .Build();
builder.Services.AddSingleton(new AppSettingsHelper(_config));

#region ���ÿ���

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

#region ��ʼ��Serilog��־
//Serilog:https://igeekfan.gitee.io/vovo-docs/dotnetcore/examples/serilog-tutorial.html
builder.Host.UseSerilog((builderContext, config) =>
{
    config
    .MinimumLevel.Warning()
    .Enrich.FromLogContext()
    .WriteTo.File(Path.Combine("Logs", @"Log.txt"), rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj} {Exception}{NewLine}");
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

#region ��ʼ��AutoMapper �Զ�ӳ��
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));
#endregion

#region ע�������
builder.Services.AddControllers(options =>
{
    //��GlobalExceptionFilter��������ӵ�������ѡ����
  //  options.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(options =>
{
    //ͨ������ JsonSerializerOptions ���Զ��� JSON �����ڸ�ʽ
    //ע�⣺����ʹ�� .NET 6 �е� System.Text.Json ʵ���Զ������ڸ�ʽ�ķ��������ϣ��ʹ�� Newtonsoft.Json�������� AddJsonOptions ��ʹ�� NewtonsoftJson ������� JsonSerializerOptions��
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd HH:mm:ss"));
});
#endregion

var app = builder.Build();

#region ȫ���쳣����
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        //��ȡ��ǰ�쳣ʵ��
        var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
        //�ж��쳣�Ƿ�Ϊ��
        var exception = exceptionHandler?.Error;
        // ��¼�쳣��־
        Log.Error(exception, "����δ������쳣");
        // �����Զ��������Ӧ
        context.Response.StatusCode = 500;
        var responseData = new ResultData
        {
            Code = ResultCode.Error,
            Message = "�����˴������Ժ����ԣ�",
            Data = new object[] { }
        };
        var jsonString = JsonSerializer.Serialize(responseData);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonString);
    });
});
#endregion

app.UseCors("myHomeAPI");
//���÷��ʾ�̬��Դ��·�����ٷ��ĵ���https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0��
app.UseStaticFiles(new StaticFileOptions
{
    //��Դ���ڵľ���·��
    FileProvider = new PhysicalFileProvider(System.IO.Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    //��ʾ����·��,����'/'��ͷ
    RequestPath = "/uploads"
});
//����·���м��������������������URL��������ָ����·�ɹ�������·�ɵ���Ӧ�Ĵ��������������ͨ��ʹ��·���м�������Զ��岻ͬ�Ķ˵��·�ɹ����Ա㽫�����͵���ȷ�Ĵ��������������
app.UseRouting();
//���������֤�м��
app.UseAuthentication();
//������Ȩ�м�����������û�����Դ�ķ���Ȩ��
app.UseAuthorization();

#region ����swaggerUI http://wwl.homeapi.com:5108/swagger/index.html
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
