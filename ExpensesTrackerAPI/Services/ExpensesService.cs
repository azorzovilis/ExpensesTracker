namespace ExpensesTrackerAPI.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using DAL;
    using ExpensesTrackerAPI.Models;
    using Microsoft.EntityFrameworkCore;

    public class ExpensesService : IExpensesService
    {
        private readonly ExpensesContext _context;
        private readonly IDataRepository<Expense> _repo;
        private readonly IMemoryCacheService<IEnumerable<ExpenseType>> _cacheService;

        public ExpensesService(ExpensesContext context,
            IDataRepository<Expense> repo,
            IMemoryCacheService<IEnumerable<ExpenseType>> cacheService)
        {
            _context = context;
            _repo = repo;
            _cacheService = cacheService;
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _context.Expenses
                .Include(et => et.ExpenseType)
                .OrderByDescending(p => p.ExpenseId);
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await _context.Expenses.FindAsync(expenseId);
        }

        public async Task<Expense> CreateExpense(Expense expense)
        {
            _repo.Add(expense);
            return await _repo.SaveAsync(expense);
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            _context.Entry(expense).State = EntityState.Modified;

            _repo.Update(expense);

            return await _repo.SaveAsync(expense);
        }

        public async Task<Expense> DeleteExpense(Expense expense)
        {
            _repo.Delete(expense);
            return await _repo.SaveAsync(expense);
        }

        public async Task<bool> ExpenseExists(int expenseId)
        {
            return await _context.Expenses.AnyAsync(e => e.ExpenseId == expenseId);
        }

        public async Task<IEnumerable<ExpenseType>> GetExpenseTypes()
        {
            const string expenseTypeKey = "ExpenseTypes";

            return await _cacheService
                .GetOrCreate(expenseTypeKey, async () => await _context.ExpenseType.ToListAsync());
        }
    }
}
