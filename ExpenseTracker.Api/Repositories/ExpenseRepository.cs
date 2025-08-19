using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Interfaces;
using ExpenseTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseDbContext _context;

        public ExpenseRepository(ExpenseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense?> UpdateAsync(int id, Expense expense)
        {
            var existingExpense = await _context.Expenses.FindAsync(id);
            if (existingExpense == null)
                return null;

            existingExpense.Title = expense.Title;
            existingExpense.Amount = expense.Amount;
            existingExpense.ExpenseDate = expense.ExpenseDate;
            existingExpense.Notes = expense.Notes;
            existingExpense.CategoryId = expense.CategoryId;

            await _context.SaveChangesAsync();
            return existingExpense;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
                return false;

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
