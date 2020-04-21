namespace ExpensesTrackerAPI.IntegrationTests.Services
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

        private readonly IDataRepository<Expense> _dataRepository = Mock.Of<IDataRepository<Expense>>();
        private readonly IMemoryCacheService<IEnumerable<ExpenseType>> _memoryCacheService =
            Mock.Of<IMemoryCacheService<IEnumerable<ExpenseType>>>();

        private DbContextOptions<ExpensesContext> _options;

        [SetUp]
        public void SetUp()
        {
            // Running the tests in parallel results in a race condition when seeding the data,
            // thus a fix until EF Core is updated to handle this scenario is to use a guid for the DB name
            // check https://stackoverflow.com/questions/33490696/how-can-i-reset-an-ef7-inmemory-provider-between-unit-tests

            _options = new DbContextOptionsBuilder<ExpensesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
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
                    _dataRepository,
                    _memoryCacheService
                );

                var result = await _itemUnderTest.GetExpenses();
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
                    _dataRepository,
                    _memoryCacheService
                );

                var result = await _itemUnderTest.GetExpense(expenseId: 2);
                Assert.AreEqual("Eliza", result.Recipient);
                Assert.AreEqual("GBP", result.Currency);
            }
        }

        [Test]
        public async Task GivenAnExpenseService_WhenCreateExpenseIsCalled_ThenNewExpenseShouldBeAdded()
        {
            //Setup
            var newExpense = new Expense { Amount = 5.6m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Artemis", TransactionDate = DateTime.MinValue };
            var emptyOptions = new DbContextOptionsBuilder<ExpensesContext>()
                .UseInMemoryDatabase("MyEmptyDB")
                .Options;

            GivenEmptyExpenseTable(emptyOptions);

            //Act
            using (var context = new ExpensesContext(emptyOptions))
            {
                _itemUnderTest = new ExpensesService(context,
                    new DataRepository<Expense>(context),
                    _memoryCacheService
                );

                var result = await _itemUnderTest.CreateExpense(newExpense);
                Assert.AreEqual("Artemis", result.Recipient);
                Assert.Positive(context.Expenses.Count());
            }
        }

        private static void GivenEmptyExpenseTable(DbContextOptions<ExpensesContext> emptyOptions)
        {
            // Insert seed data into the database using one instance of the context
            using (var context = new ExpensesContext(emptyOptions))
            {
                context.ExpenseType.Add(new ExpenseType { ExpenseTypeId = ExpenseTypeId.Food, Description = "Food" });
                context.ExpenseType.Add(new ExpenseType { ExpenseTypeId = ExpenseTypeId.Drinks, Description = "Drinks" });

                context.SaveChanges();
            }
        }
    }
}