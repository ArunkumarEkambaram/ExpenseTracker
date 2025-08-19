using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Interfaces
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync();
        Task<ExpenseDto?> GetExpenseByIdAsync(int id);
        Task<ExpenseDto> CreateExpenseAsync(AddOrUpdateExpenseDto createExpenseDto);
        Task<ExpenseDto?> UpdateExpenseAsync(int id, AddOrUpdateExpenseDto updateExpenseDto);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
