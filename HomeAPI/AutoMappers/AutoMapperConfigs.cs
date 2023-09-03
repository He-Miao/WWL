using AutoMapper;
using Common.Helpers;
using HomeAPI.Model.Dtos;
using HomeAPI.Model.Models;

namespace HomeAPI.AutoMappers
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public class AutoMapperConfigs : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public AutoMapperConfigs()
        {
            CreateMap<Expense, ExpenseDto>();
//            CreateMap<ExpenseDto, Expense>()
//.BeforeMap((src, dest) => src.AuditState = src.AuditState.IsNumeric() ? src.AuditState : EnumHelper.GetEnumValueFromDescription<AuditState>(src.AuditState).ToString())
//.BeforeMap((src, dest) => src.Classify = src.Classify.IsNumeric() ? src.Classify : EnumHelper.GetEnumValueFromDescription<BillType>(src.Classify).ToString());
//            CreateMap<ExpenseDto, Expense>()
//            .BeforeMap((src, dest) => src.AuditState = src.AuditState.IsNumeric() ? src.AuditState : EnumHelper.GetEnumValueFromDescription<AuditState>(src.AuditState).ToString())
//            .BeforeMap((src, dest) => src.Classify = src.Classify.IsNumeric() ? src.Classify : EnumHelper.GetEnumValueFromDescription<BillType>(src.Classify).ToString());
            CreateMap<Expense, ExpenseDto>()
             .ForMember(dest => dest.PayDate, opt => opt.MapFrom(src => src.PayDate.DateTimeToString()))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime.DateTimeToString()));
        }
    }
}
