namespace ExpensesTrackerAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DAL;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpensesContext _context;
        private readonly IDataRepository<Expense> _repo;

        public ExpensesController(ExpensesContext context, IDataRepository<Expense> repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: api/expenses
        [HttpGet]
        public IEnumerable<Expense> GetExpenses()
        {
            return _context.Expenses.OrderByDescending(p => p.ExpenseId);
        }

        // GET: api/expenses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = await _context.Expenses.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense([FromRoute] int id, [FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                _repo.Update(expense);
                var save = await _repo.SaveAsync(expense);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<IActionResult> PostExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(expense);
            var save = await _repo.SaveAsync(expense);

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

        // DELETE: api/expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _repo.Delete(expense);
            var save = await _repo.SaveAsync(expense);

            return Ok(expense);
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
