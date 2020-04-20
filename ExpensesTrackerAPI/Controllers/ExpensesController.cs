namespace ExpensesTrackerAPI.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesService _expensesService;

        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        [HttpGet]
        public IEnumerable<Expense> GetExpenses()
        {
            return _expensesService.GetExpenses();
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
            catch(DbUpdateConcurrencyException)
            {
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
