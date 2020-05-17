namespace ExpensesTrackerAPI.IntegrationTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;
    using Xunit;
    public class ExpensesControllerTests : IClassFixture<AppTestFixture<Startup>>
    {
        private readonly HttpClient _client;

        public ExpensesControllerTests(AppTestFixture<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async void GivenExpensesEndpoint_WhenGetExpensesIsCalled_ThenExpenseListShouldBeReturned()
        {
            var response = await _client.GetAsync("/api/expenses");
            response.EnsureSuccessStatusCode();

            var expenses = JsonConvert.DeserializeObject<List<Expense>>(
                await response.Content.ReadAsStringAsync());

            Assert.NotEmpty(expenses);
            Assert.Contains(expenses, x => x.Recipient == "Alex");
        }

        [Fact]
        public async void GivenExpensesEndpoint_WhenGetExpenseIsCalled_ThenExpenseShouldBeReturned()
        {
            const int expenseId = 3;

            var response = await _client.GetAsync("/api/expenses/" + expenseId);
            response.EnsureSuccessStatusCode();

            var expense = JsonConvert.DeserializeObject<Expense>(
                await response.Content.ReadAsStringAsync());

            Assert.Equal("Artemis", expense.Recipient);
        }

        [Fact]
        public async void GivenExpensesEndpoint_WhenGetInvalidExpenseIsCalled_ThenResponseShouldBeNotFound()
        {
            const int invalidExpenseId = int.MinValue;

            var response = await _client.GetAsync("/api/expenses/" + invalidExpenseId);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void GivenExpensesEndpoint_WhenPostExpensesIsCalled_ThenNewExpenseShouldBeCreated()
        {
            var newExpense = new Expense { Amount = 15.6m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Nick", TransactionDate = new DateTime(2020, 1, 17) };

            var content = new StringContent(JsonConvert.SerializeObject(newExpense), System.Text.Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/expenses", content);

            var createdExpense = JsonConvert.DeserializeObject<Expense>(
                await response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Nick", createdExpense.Recipient);
        }
    }
}
