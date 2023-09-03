using AutoMapper;
using Common.Helpers;
using Common.Extensions;
using HomeAPI.IRepository;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Enums;
using HomeAPI.Model.Models;
using SqlSugar;
using System.Linq.Expressions;
using Common.Paging;

namespace HomeAPI.Service
{
    /// <summary>
    /// 支出业务逻辑层实现类
    /// </summary>
    public class ExpenseService : BaseService<Expense>, IExpenseService
    {
        IExpenseRepository _expenseRepository;
        IMapper _mapper;
        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper) : base(expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
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
        public async Task<ResultData> GetExpenseList(string? keyword, DateTime? startTime, DateTime? endTime, int classify = 0, int auditState = 0, int currentPage = 1, int pageSize = 10)
        {
            #region 构建查询表达式
            var expable = Expressionable.Create<Expense>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expable = expable.And(a => a.Title.Contains(keyword));
            }
            if (classify > 0)
            {
                expable = expable.And(a => a.Classify == classify);
            }
            if (auditState > 0)
            {
                expable = expable.And(a => a.AuditState == auditState);
            }
            if (startTime != null)
            {
                expable = expable.And(a => a.PayDate >= startTime);
            }
            if (endTime != null)
            {
                expable = expable.And(a => a.PayDate <= endTime);
            }
            #endregion
            ResultData data = await QueryableAsync(expable.ToExpression());
            //类型转换
            List<Expense> expenseList = data.Data.ToList<Expense>();
            expenseList = expenseList.OrderByDescending(o => o.PayDate).ToList();
            //总金额
            string totalAmount = Utils.ConvertToYuan(expenseList.Sum(s => s.Amount)).ToString();
            //本月
            string monthAmount = Utils.ConvertToYuan(expenseList.Where(w => w.PayDate.ToString("yyyy-MM") == DateTime.Now.ToString("yyyy-MM")).Sum(s => s.Amount)).ToString();
            //当天
            string dayAmount = Utils.ConvertToYuan(expenseList.Where(w => w.PayDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).Sum(s => s.Amount)).ToString();
            var pagedExpense =  PagingExtensions.ToPagedList<Expense>(expenseList, currentPage, pageSize);
            if (pagedExpense.Data.Count()>0)
            {
                //将Expense对象映射为ExpenseDto对象
                List<ExpenseDto> expenseDtoList = _mapper.Map<List<ExpenseDto>>(pagedExpense.Data);
                data.Data = new
                {
                    expenseList = expenseDtoList,
                    total = pagedExpense.TotalItemCount,
                    totalAmount,
                    monthAmount,
                    dayAmount
                };
            }
            return data;
        }

        /// <summary>
        /// 修改支出记录
        /// </summary>
        /// <param name="expense">支出实体</param>
        /// <returns></returns>
        public ResultData ModifyExpense(ExpenseDto expense)
        {
            var expenseModel = _mapper.Map<Expense>(expense);
            // 创建一个忽略字段的表达式
            Expression<Func<Expense, object>> ignoreColumns = entity => new
            {
                entity.CreationTime
            };
            ResultData data = Update(expenseModel, ignoreColumns);
            return data;
        }

        /// <summary>
        /// 获取支出图表数据
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        public ResultData GetExpenseChat(string year, int month)
        {
            string[] arrayYear = year.Split('-');
            ResultData result = new ResultData();
            //获取收支集合数据
            ResultData data = Queryable();
            List<Expense>? expenseList = data.Data as List<Expense>;
            if (expenseList != null)
            {
                List<object> chartData = new List<object>();
                List<object> expenseData = new List<object>();
                List<IGrouping<int, Expense>> expenseGroup;
                //获取指定年份数据
                if (arrayYear.Length == 2 && month < 1)
                {
                    //按年份分组
                    expenseGroup = expenseList.GroupBy(g => g.PayDate.Year).ToList();
                    expenseGroup = expenseGroup.Where(w => w.Key > Convert.ToInt32(arrayYear.FirstOrDefault()) && w.Key < Convert.ToInt32(arrayYear.LastOrDefault())).ToList();
                    foreach (var item in expenseGroup)
                    {
                        chartData.Add(new
                        {
                            key = item.Key + "年",
                            amount = (decimal)item.Sum(s => s.Amount) / 100
                        });
                    }
                }
                else//获取指定月份数据
                {
                    //按月份分组
                    expenseGroup = expenseList.Where(w => w.PayDate.Year == Convert.ToInt32(arrayYear.FirstOrDefault()) && w.PayDate.Month == month).GroupBy(g => g.PayDate.Day).OrderBy(o => o.Key).ToList();
                    foreach (var item in expenseGroup)
                    {
                        chartData.Add(new
                        {
                            key = item.Key + "号",
                            amount = (decimal)item.Sum(s => s.Amount) / 100
                        });
                    }
                }
                //按类型分组
                List<IGrouping<int, Expense>> classifyGroup = expenseList.GroupBy(g => g.Classify).ToList();
                foreach (var item in classifyGroup)
                {
                    expenseData.Add(new
                    {
                        key = item.Key,
                        name = EnumHelper.GetEnumDescription((BillType)item.Key),
                        amount = (decimal)item.Sum(s => s.Amount) / 100
                    });
                }
                result.Data = new { chatData = chartData, expenseData };
            }
            return result;
        }
    }
}
