using AutoMapper;
using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Mappings
{
    public class ExpenseMappingProfile : Profile
    {
        public ExpenseMappingProfile()
        {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category!.CategoryName));

            CreateMap<AddOrUpdateExpenseDto, Expense>();
        }
    }
}
