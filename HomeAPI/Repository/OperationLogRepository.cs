using AutoMapper;
using HomeAPI.Model.Models;
using SqlSugar;

namespace HomeAPI.Repositories
{
    /// <summary>
    /// 日志仓储
    /// </summary>
    public class OperationLogRepository : BaseRepository<OperationLog, OperationLog>
    {
        public OperationLogRepository(IMapper mapper, SqlSugarScope sqlSugar) : base(sqlSugar)
        {

        }
    }
}
