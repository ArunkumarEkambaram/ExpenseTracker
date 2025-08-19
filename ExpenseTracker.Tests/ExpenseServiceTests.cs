
using AutoMapper;
using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Interfaces;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Services;
using FluentAssertions;
using Moq;

namespace ExpenseTracker.Tests
{
    public class ExpenseServiceTests
    {
        private readonly ExpenseService _service;
        private readonly Mock<IExpenseRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;

        //Setup
        public ExpenseServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IExpenseRepository>();
            _service = new ExpenseService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllExpensesAsync_ReturnAllExpenses()
        {
            //Arrange
            var expenses = new List<Expense>
            {
                new Expense {Id=1, Title="Title1", Amount=100, ExpenseDate=DateTime.Now, CategoryId=1},
                new Expense {Id=2, Title="Title2", Amount=200, ExpenseDate=DateTime.Now, CategoryId=2},
            };
            var expenseDto = new List<ExpenseDto>
            {
                new ExpenseDto {Id=1, Title="Title1", Amount=100, ExpenseDate=DateTime.Now, CategoryId=1 },
                new ExpenseDto {Id=2, Title="Title2", Amount=200, ExpenseDate=DateTime.Now, CategoryId=2},
            };

            _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(expenses);
            _mockMapper.Setup(x => x.Map<IEnumerable<ExpenseDto>>(expenses)).Returns(expenseDto);

            //Act
            var result = await _service.GetAllExpensesAsync();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ValidId_ReturnsExpense()
        {
            //Arrange
            var expense = new Expense { Id = 1, Title = "Title1", Amount = 100, ExpenseDate = DateTime.Now, CategoryId = 1 };
            var expenseDto = new ExpenseDto { Id = 1, Title = "Title1", Amount = 100, ExpenseDate = DateTime.Now, CategoryId = 1 };

            _mockRepository.Setup(x => x.GetByIdAsync(expense.Id)).ReturnsAsync(expense);
            _mockMapper.Setup(x => x.Map<ExpenseDto>(expense)).Returns(expenseDto);

            //Act
            var result = await _service.GetExpenseByIdAsync(expense.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, expense.Id);
            Assert.Equal(result.Amount, expense.Amount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task GetExpenseByIdAsync_LessThanZero_ThrowsArgumentException(int x)
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetExpenseByIdAsync(x));
        }

        [Fact]
        public async Task GetExpenseByIdAsync_InvalidId_ReturnsNull()
        {
            Expense expense = null!;
            ExpenseDto expenseDto = null!;
            _mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(expense);
            _mockMapper.Setup(x => x.Map<ExpenseDto>(expense)).Returns(expenseDto);

            var result = await _service.GetExpenseByIdAsync(1);

            Assert.Null(result);
        }
    }
}
