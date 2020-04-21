namespace ExpensesTrackerAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult Currencies()
        {
            var currencies = _currencyService.GetCurrencies();

            return Ok(currencies);
        }
    }
}
