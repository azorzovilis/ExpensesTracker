namespace ExpensesTrackerAPI.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using DAL;
    using ExpensesTrackerAPI.Services;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ExpensesServiceTests
    {
        private IExpensesService _itemUnderTest;

        private DbContextOptions<ExpensesContext> _options;

        [OneTimeSetUp]
        public void SetUp()
        {
            _options = new DbContextOptionsBuilder<ExpensesContext>()
                .UseInMemoryDatabase(databaseName: "ExpensesTrackerDB")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ExpensesContext(_options))
            {
                context.ExpenseType.Add(new ExpenseType { ExpenseTypeId = ExpenseTypeId.Food, Description = "Food" });
                context.ExpenseType.Add(new ExpenseType { ExpenseTypeId = ExpenseTypeId.Drinks, Description = "Drinks" });

                context.Expenses.Add(new Expense { ExpenseId = 1, Amount = 10.4m, Currency = "CHF", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Alex", TransactionDate = DateTime.MinValue });
                context.Expenses.Add(new Expense { ExpenseId = 2, Amount = 5.6m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Eliza", TransactionDate = DateTime.MinValue });

                context.SaveChanges();
            }
        }

        [Test]
        public async Task GivenAnExpenseService_WhenGetExpensesIsCalled_ThenExpenseListShouldBeReturned()
        {
            // Use a clean instance of the context to run the test
            using (var context = new ExpensesContext(_options))
            {
                _itemUnderTest = new ExpensesService(context,
                    Mock.Of<IDataRepository<Expense>>(),
                    Mock.Of<IMemoryCacheService<IEnumerable<ExpenseType>>>()
                );

                var result =  await _itemUnderTest.GetExpenses();
                Assert.AreEqual(2, result.Count());
            }
        }

        [Test]
        public async Task GivenAnExpenseService_WhenGetExpenseIsCalled_ThenExpenseShouldBeReturned()
        {
            // Use a clean instance of the context to run the test
            using (var context = new ExpensesContext(_options))
            {
                _itemUnderTest = new ExpensesService(context,
                    Mock.Of<IDataRepository<Expense>>(),
                    Mock.Of<IMemoryCacheService<IEnumerable<ExpenseType>>>()
                );

                var result = await _itemUnderTest.GetExpense(expenseId: 2);
                Assert.AreEqual("Eliza", result.Recipient);
                Assert.AreEqual("GBP", result.Currency);
            }
        }
    }
}