namespace ExpensesTrackerAPI.Services
{
    using System.Collections.Generic;
    using Models;

    public interface ICurrencyService
    {
        IEnumerable<Currency> GetCurrencies();

        bool IsValidCurrency(string currencyISO);
    }
}
