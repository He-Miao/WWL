using AutoMapper;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace HomeAPI.Controllers
{
    /// <summary>
    /// 收支控制器
    /// </summary>
    [Route("[controller]")]
    public class BillController : BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        public BillController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        /// <summary>
        /// 新增支出记录
        /// </summary>
        /// <param name="expense">支出实体</param>
        /// <returns></returns>
        [HttpPost("AddExpenseList")]
        public IActionResult AddExpenseList(ExpenseDto expense)
        {
            var expenseModel = _mapper.Map<Expense>(expense);
            ResultData data = _expenseService.AddScalar(expenseModel);
            return Ok(data);
        }

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
        [HttpGet("GetExpenseList")]
        public ResultData GetExpenseList(string? keyword, DateTime? startTime, DateTime? endTime, int classify = 0, int auditState = 0, int currentPage = 1,int pageSize = 10)
        {
            #region 构建查询表达式
            var expable = Expressionable.Create<Expense>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expable = expable.And(a => a.Title.Contains(keyword));
            }
            if (classify>0)
            {
                expable = expable.And(a => a.Classify == classify);
            }
            if (auditState > 0)
            {
                expable = expable.And(a => a.AuditState == auditState);
            }
            if (startTime != null)
            {
                expable = expable.And(a => a.PayDate < startTime);
            }
            if (endTime != null)
            {
                expable = expable.And(a => a.PayDate > endTime);
            }
            #endregion
            ResultData data = _expenseService.QueryableByPage(expable.ToExpression(), it => it.ExpenseId, true, currentPage, pageSize);
            return data;
        }

        /// <summary>
        /// 删除支出数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete("DeleteExpense")]
        public ResultData DeleteExpense(int id)
        {           
            ResultData data = _expenseService.Delete(id);
            return data;
        }

        /// <summary>
        /// 修改支出记录
        /// </summary>
        /// <param name="expense">支出实体</param>
        /// <returns></returns>
        [HttpPatch("ModifyExpense")]
        public IActionResult ModifyExpense(ExpenseDto expense)
        {
            var expenseModel = _mapper.Map<Expense>(expense);
            expenseModel.CreationTime = DateTime.Now;
            ResultData data = _expenseService.Update(expenseModel);
            return Ok(data);
        }
    }
}
