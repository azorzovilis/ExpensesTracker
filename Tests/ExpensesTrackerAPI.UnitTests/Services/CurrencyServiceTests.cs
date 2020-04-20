namespace ExpensesTrackerAPI.UnitTests.Services
{
    using System.Linq;
    using ExpensesTrackerAPI.Services;
    using NUnit.Framework;

    [TestFixture]
    public class CurrencyServiceTests
    {
        private ICurrencyService _itemUnderTest;

        [SetUp]
        public void Setup()
        {
            _itemUnderTest = new CurrencyService();
        }

        [Test]
        public void GivenACurrencyService_WhenGetCurrenciesIsCalled_ThenCurrencyListShouldBeReturned()
        {
            var result = _itemUnderTest.GetCurrencies();

            Assert.NotZero(result.Count());
        }

        [Test]
        public void GivenAnInvalidCurrencyCode_WhenIsValidCurrencyIsCalled_ThenResultShouldBeFalse()
        {
            const string invalidCurrencyISOCode = "InvalidISOCode";

            var result = _itemUnderTest.IsValidCurrency(invalidCurrencyISOCode);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("CHF")]
        [TestCase("EUR")]
        [TestCase("GBP")]
        [TestCase("USD")]
        public void GivenAValidCurrencyCode_WhenIsValidCurrencyIsCalled_ThenResultShouldBeTrue(string ISOCode)
        {
            var result = _itemUnderTest.IsValidCurrency(ISOCode);

            Assert.IsTrue(result);
        }
    }
}