using Common.Enums;
using Common.Helpers;
using HomeAPI.Model.Models;
using HomeAPI.Repositories;
using Jaina;

namespace HomeAPI.Subscribers
{
    /// <summary>
    /// 日志订阅
    /// </summary>
    public class LogSubscriber : IEventSubscriber
    {
        readonly OperationLogRepository _logRep;
        public LogSubscriber(OperationLogRepository logRep)
        {
            _logRep = logRep;
        }

        [EventSubscribe(SubscribeType.AuditLogs)]
        public async Task LogEvent(EventHandlerExecutingContext context)
        {
            var log = context.Source.Payload.ToString().ToObject<OperationLog>();
            //分表插入日志
            await _logRep.AddSplitTableConcurrentAsync(log);
        }
    }
}
