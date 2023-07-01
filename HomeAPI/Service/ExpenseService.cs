using HomeAPI.IRepository;
using HomeAPI.IService;
using HomeAPI.Model.Models;

namespace HomeAPI.Service
{
    /// <summary>
    /// 支出业务逻辑层实现类
    /// </summary>
    public class ExpenseService:BaseService<Expense>,IExpenseService
    {
        IExpenseRepository _expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository):base(expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
    }
}
