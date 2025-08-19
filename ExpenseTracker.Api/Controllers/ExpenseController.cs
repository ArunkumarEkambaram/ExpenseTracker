using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpenses()
        {
            var expenses = await _service.GetAllExpensesAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDto>> GetExpense(int id)
        {
            var expense = await _service.GetExpenseByIdAsync(id);
            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> CreateExpense(AddOrUpdateExpenseDto createExpenseDto)
        {
            var createdExpense = await _service.CreateExpenseAsync(createExpenseDto);
            return CreatedAtAction(nameof(GetExpense), new { id = createdExpense.Id }, createdExpense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExpenseDto>> UpdateExpense(int id, AddOrUpdateExpenseDto updateExpenseDto)
        {
            var updatedExpense = await _service.UpdateExpenseAsync(id, updateExpenseDto);
            if (updatedExpense == null)
                return NotFound();

            return Ok(updatedExpense);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            var deleted = await _service.DeleteExpenseAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
