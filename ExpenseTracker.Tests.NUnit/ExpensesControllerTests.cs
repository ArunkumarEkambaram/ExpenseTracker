using ExpenseTracker.Api.Controllers;
using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ExpenseTracker.Tests.NUnit
{
    public class ExpensesControllerTests
    {
        private Mock<IExpenseService> _mockService;
        private ExpenseController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IExpenseService>();
            _controller = new ExpenseController(_mockService.Object);
        }

        [Test]
        public async Task GetExpense_ValidId_ReturnsExpense()
        {
            var expenseDto = new ExpenseDto { Id = 1, Title = "Title1", Amount = 100, ExpenseDate = DateTime.Now, CategoryId = 1 };

            _mockService.Setup(x => x.GetExpenseByIdAsync(1)).ReturnsAsync(expenseDto);

            var result = await _controller.GetExpense(1);

            var okResult = (OkObjectResult)result.Result;
            Assert.That(okResult, Is.TypeOf<OkObjectResult>());//Result is OK
            var actual = okResult.Value as ExpenseDto;
            Assert.That(actual?.Id, Is.EqualTo(1));//Original Value returned by OK
        }

        [Test]
        public async Task GetExpense_InvalidId_NotFoundResult()
        {
            var expenseDto = new ExpenseDto { Id = 1, Title = "Title1", Amount = 100, ExpenseDate = DateTime.Now, CategoryId = 1 };
            _mockService.Setup(x => x.GetExpenseByIdAsync(1)).ReturnsAsync(expenseDto);

            var result = await _controller.GetExpense(2);


            var notFoundResult = (NotFoundResult)result.Result;
            Assert.That(notFoundResult, Is.TypeOf<NotFoundResult>());
        }
    }
}
