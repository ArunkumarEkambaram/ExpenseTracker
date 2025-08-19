namespace ExpenseTracker.Api.DTOs
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string? Notes { get; set; }
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
