namespace ExpensesTrackerAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expensesService;
        private readonly ILogger<ExpensesController> _logger;

        public ExpensesController(IExpensesService expensesService,
            ILogger<ExpensesController> logger)
        {
            _expensesService = expensesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _expensesService.GetExpenses();

            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = await _expensesService.GetExpense(id);

            if (expense == null)
            {
                _logger.LogInformation($"Failed to retrieve expense with id={id}");
                return NotFound();
            }

            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense([FromRoute] int id, [FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            try
            {
                await _expensesService.UpdateExpense(expense);
            }
            catch(DbUpdateConcurrencyException e)
            {
                _logger.LogError(e, "Concurrency conflicts detected", expense);

                var expenseExists = await _expensesService.ExpenseExists(id);

                if (!expenseExists)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newExpense = await _expensesService.CreateExpense(expense);

            return CreatedAtAction("GetExpense", new { id = newExpense.ExpenseId }, newExpense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = await _expensesService.GetExpense(id);
            if (expense == null)
            {
                _logger.LogInformation($"Failed to delete expense with id={id}");
                return NotFound();
            }

            await _expensesService.DeleteExpense(expense);

            return Ok();
        }

        [HttpGet]
        [Route("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var expenseTypes = await _expensesService.GetExpenseTypes();

            return Ok(expenseTypes);
        }
    }
}