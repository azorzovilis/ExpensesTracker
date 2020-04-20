namespace ExpensesTrackerAPI.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using DAL;
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
        private readonly ExpensesContext _context;
        private readonly IDataRepository<Expense> _repo;
        private readonly IMemoryCacheService<IEnumerable<ExpenseType>> _cacheService;

        public ExpensesController(IExpensesService expensesService,
            ExpensesContext context,
            IDataRepository<Expense> repo,
            IMemoryCacheService<IEnumerable<ExpenseType>> cacheService)
        {
            _expensesService = expensesService;
            _context = context;
            _repo = repo;
            _cacheService = cacheService;
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

        [HttpGet]
        [Route("Types")]
        public async Task<IActionResult> GetExpenseTypes()
        { 
            var expenseTypes = await _cacheService
                .GetOrCreate("ExpenseTypes", async () => await _context.ExpenseType.ToListAsync());

            return Ok(expenseTypes);
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.ExpenseId == id);
        }
    }
}
