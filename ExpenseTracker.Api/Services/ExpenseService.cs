using AutoMapper;
using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Interfaces;
using ExpenseTracker.Api.Models;

namespace ExpenseTracker.Api.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repository;
        private readonly IMapper _mapper;

        public ExpenseService(IExpenseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync()
        {
            var expenses = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ExpenseDto>>(expenses);
        }

        public async Task<ExpenseDto?> GetExpenseByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            var expense = await _repository.GetByIdAsync(id);
            return expense == null ? null : _mapper.Map<ExpenseDto>(expense);
        }

        public async Task<ExpenseDto> CreateExpenseAsync(AddOrUpdateExpenseDto createExpenseDto)
        {
            ValidateExpense(createExpenseDto);

            var expense = _mapper.Map<Expense>(createExpenseDto);
            var createdExpense = await _repository.CreateAsync(expense);
            return _mapper.Map<ExpenseDto>(createdExpense);
        }

        public async Task<ExpenseDto?> UpdateExpenseAsync(int id, AddOrUpdateExpenseDto updateExpenseDto)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            ValidateExpense(updateExpenseDto);

            var expense = _mapper.Map<Expense>(updateExpenseDto);
            var updatedExpense = await _repository.UpdateAsync(id, expense);
            return updatedExpense == null ? null : _mapper.Map<ExpenseDto>(updatedExpense);
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            return await _repository.DeleteAsync(id);
        }

        private void ValidateExpense(AddOrUpdateExpenseDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Expense data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title is required and cannot be empty.", nameof(dto.Title));

            if (dto.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(dto.Amount));

            if (dto.CategoryId <= 0)
                throw new ArgumentException("CategoryId must be greater than zero.", nameof(dto.CategoryId));

            if (dto.ExpenseDate == default(DateTime))
                throw new ArgumentException("ExpenseDate is required.", nameof(dto.ExpenseDate));
        }
    }

}
