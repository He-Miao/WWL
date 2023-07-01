using Common.Enums;
using Common.Helpers;
using HomeAPI.Attributes;
using HomeAPI.Model.Models;
using HomeAPI.Tools;
using Jaina;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace HomeAPI.Filters
{
    /// <summary>
    /// 日志过滤器
    /// </summary>
    public class LogActionFilter : IAsyncActionFilter
    {
        readonly IEventPublisher _eventPublisher;
        public LogActionFilter(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(a => a.GetType() == typeof(NoLogAttribute)))
            {
                return next();
            }
            return LogAsync(context, next);
        }

        private async Task LogAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sw = new Stopwatch();
            sw.Start();
            var actionResult = (await next()).Result;
            sw.Stop();

            var args = context.ActionArguments.ToJson();
            var result = ((ObjectResult)actionResult)?.Value?.ToJson();
            var request = BuilderExtensions.serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext?.Request;
            var ua = request?.Headers["User-Agent"];
            var client = UAParser.Parser.GetDefault().Parse(ua);
            var controller = ((ControllerActionDescriptor)context.ActionDescriptor).ControllerName.ToLower();
            var action = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName.ToLower();

            var log = new OperationLog
            {
                ApiPath = $"/{controller}/{action}",
                ElapsedMilliseconds = sw.ElapsedMilliseconds.ToString(),
                Params = args,
                Result = result,
                Os = client.OS.ToString(),
                DeviceInfo = string.Format("请求设备：{0}，浏览器：{1}，{2}", client.Device.ToString(), client.UA.ToString(), ua),
                IP = Utils.GetIP(request)
            };

            //推入消息总线
            await _eventPublisher.PublishAsync(SubscribeType.AuditLogs, log.ToJson());
        }
    }
}
