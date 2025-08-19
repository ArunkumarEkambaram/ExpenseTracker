namespace ExpenseTracker.Api.DTOs
{
    public class AddOrUpdateExpenseDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public int CategoryId { get; set; }
    }
}
