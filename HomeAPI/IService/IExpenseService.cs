
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
        /// 修改支出记录
        /// </summary>
        /// <param name="expense">支出实体</param>
        /// <returns></returns>
       ResultData ModifyExpense(ExpenseDto expense);
    }
}
