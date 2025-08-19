using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Interfaces
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync();
        Task<Expense?> GetByIdAsync(int id);
        Task<Expense> CreateAsync(Expense expense);
        Task<Expense?> UpdateAsync(int id, Expense expense);
        Task<bool> DeleteAsync(int id);
    }
}
