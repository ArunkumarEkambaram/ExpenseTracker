using AutoMapper;
using ExpenseTracker.Api.DTOs;
using ExpenseTracker.Api.Interfaces;
using ExpenseTracker.Api.Models;
using ExpenseTracker.Api.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Tests.NUnit
{
    [TestFixture]
    public class ExpenseServiceTests
    {
        private ExpenseService _service;
        private Mock<IExpenseRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {
            _mockMapper = new Mock<IMapper>();
            _mockRepository = new Mock<IExpenseRepository>();
            _service = new ExpenseService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
