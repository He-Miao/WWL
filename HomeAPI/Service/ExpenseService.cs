using AutoMapper;
using HomeAPI.IRepository;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Linq.Expressions;

namespace HomeAPI.Service
{
    /// <summary>
    /// 支出业务逻辑层实现类
    /// </summary>
    public class ExpenseService:BaseService<Expense>,IExpenseService
    {
        IExpenseRepository _expenseRepository;
        IMapper _mapper;
        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper) :base(expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
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
    }
}
