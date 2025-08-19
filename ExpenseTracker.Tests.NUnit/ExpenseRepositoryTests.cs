using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Tests.NUnit
{
    [TestFixture]
    public class ExpenseRepositoryTests
    {
        private ExpenseDbContext _context;
        private ExpenseRepository _repository;

        [SetUp]
        public void SetUp()
        {
            var option = new DbContextOptionsBuilder<ExpenseDbContext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .Options;
            _context = new ExpenseDbContext(option);
            _repository = new ExpenseRepository(_context);

            var category = new Category { Id = 1, CategoryName = "Grocery" };
            var expenses = new List<Expense>
            {
                 new Expense {Id=1, Title="Title1", Amount=100, ExpenseDate=DateTime.Now, CategoryId=1},
                 new Expense {Id=2, Title="Title2", Amount=200, ExpenseDate=DateTime.Now, CategoryId=1},
            };

            _context.Categories.Add(category);
            _context.Expenses.AddRange(expenses);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnExpense()
        {
            var result = await _repository.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Title, Is.EqualTo("Title1"));
        }

        [Test]
        public async Task CreateAsync_ValidExpense_ReturnsValidExpense()
        {
            var expense = new Expense { Id = 3, Title = "Title3", Amount = 1000, CategoryId = 1, ExpenseDate = DateTime.Now };
            var actualResult = await _repository.CreateAsync(expense);

            Assert.That(actualResult.Id, Is.EqualTo(3));
            var result = _context.Expenses.FirstOrDefault(x => x.Id == expense.Id);
            Assert.That(actualResult.Id, Is.EqualTo(result?.Id));
        }
    }
}
