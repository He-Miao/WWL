
using HomeAPI.Model;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;

namespace HomeAPI.IService
{
    /// <summary>
    /// 支出业务逻辑层接口
    /// </summary>
    public interface IExpenseService:IBaseService<Expense>
    {
        /// <summary>
        /// 获取支出列表
        /// </summary>
        /// <param name="keyword">搜索关键词（标题）</param>
        /// <param name="classify">分类</param>
        /// <param name="auditState">审核状态</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="currentPage">当前页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
         Task<ResultData> GetExpenseList(string? keyword, DateTime? startTime, DateTime? endTime, int classify = 0, int auditState = 0, int currentPage = 1, int pageSize = 10);

        /// <summary>
        /// 修改支出记录
        /// </summary>
        /// <param name="expense">支出实体</param>
        /// <returns></returns>
       ResultData ModifyExpense(ExpenseDto expense);
        /// <summary>
        /// 获取支出图表数据
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        ResultData GetExpenseChat(string year, int month);
    }
}
