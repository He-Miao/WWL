using HomeAPI.IRepository;
using HomeAPI.Model.Models;
using SqlSugar;

namespace HomeAPI.Repository
{
    public class ExpenseRepository:BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(SqlSugarScope sqlSugar) : base(sqlSugar)
        {

        }
    }
}
