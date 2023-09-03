using AutoMapper;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    /// <summary>
    /// 收支控制器
    /// </summary>
    [Route("homeapi/[controller]")]
    [ApiController]
    public class BillController : BaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expenseService">收支服务对象</param>
        /// <param name="mapper"></param>
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
        public IActionResult AddExpenseList(Expense expense)
        {
            ResultData data = _expenseService.AddScalar(expense);
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
        public async Task<ResultData> GetExpenseList(string? keyword, DateTime? startTime, DateTime? endTime, int classify = 0, int auditState = 0, int currentPage = 1,int pageSize = 10)
        {        
            ResultData data =await _expenseService.GetExpenseList(keyword, startTime, endTime, classify, auditState, currentPage, pageSize);
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
        /// 批量删除支出数据
        /// </summary>
        /// <param name="keys">id集合</param>
        /// <returns></returns>
        [HttpPost("DeleteExpenseList")]
        public ResultData DeleteExpenseList(List<dynamic> keys)
        {
            ResultData data = _expenseService.Delete(keys);
            return data;
        }

        /// <summary>
        /// 修改支出记录
        /// </summary>
        /// <param name="expenseDto">支出实体</param>
        /// <returns></returns>
        [HttpPatch("ModifyExpense")]
        public IActionResult ModifyExpense(Expense expense)
        {
            ResultData data = _expenseService.Update(expense);
            return Ok(data);
        }


        #region chart图表数据
        /// <summary>
        /// 获取支出图表数据
        /// </summary>
        /// <param name="year">年（参数形式为2020,2021）</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [HttpGet("GetExpenseChat")]
        public IActionResult GetExpenseChat(string year, int month)
        {
            return Ok(_expenseService.GetExpenseChat(year, month));
        }
        #endregion
    }
}
