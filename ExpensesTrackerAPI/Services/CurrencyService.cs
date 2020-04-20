namespace ExpensesTrackerAPI.Services
{
    using ExpensesTrackerAPI.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;

    public class CurrencyService : ICurrencyService
    {
        private readonly IList<Currency> _currencies;
        private IDictionary<string, string> _currencyMap;

        public CurrencyService()
        {
            _currencies = GetCurrencyRegionInfo();
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return _currencies;
        }

        public bool IsValidCurrency(string currencyISO)
        {
            if (_currencyMap == null)
            {
                _currencyMap = _currencies.ToDictionary(c => c.ISO, c => c.Name);
            }

            return _currencyMap.ContainsKey(currencyISO);
        }

        private static IList<Currency> GetCurrencyRegionInfo()
        {
           return CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(culture => {
                    try
                    {
                        return new RegionInfo(culture.Name);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(ri => ri != null)
                .OrderBy(ri => ri.ISOCurrencySymbol)
                .GroupBy(ri => ri.ISOCurrencySymbol)
                .Select(cur => new Currency { ISO = cur.Key, Name = cur.First().CurrencyEnglishName })
                .ToList();
        }
    }
}
