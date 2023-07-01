using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    /// <summary>
    /// 收支控制器
    /// </summary>
    [Route("api/[controller]")]
    public class BillController : BaseController
    {
        private readonly IExpenseService _expenseService;
        public BillController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        /// <summary>
        /// 新增支出记录
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddExpenseList(Expense expense)
        {
            ResultData data = _expenseService.AddScalar(expense);
            return JsonResult(data.Code, data.Msg,null);
        }

        /// <summary>
        /// 获取支出列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetExpenseList()
        {
            ResultData data = _expenseService.Queryable();
            return JsonResult(data.Code, data.Msg, data.Data);
        }
    }
}
