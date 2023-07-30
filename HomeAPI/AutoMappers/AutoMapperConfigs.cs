using AutoMapper;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;

namespace HomeAPI.AutoMappers
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public class AutoMapperConfigs : Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<Expense, ExpenseDto>();
            CreateMap<ExpenseDto, Expense>();
        }
    }
}
