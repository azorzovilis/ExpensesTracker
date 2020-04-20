namespace ExpensesTrackerAPI.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Cache;
    using DAL;
    using ExpensesTrackerAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;

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
            //return _context.Expenses;

            return _context.Expenses
                .Include(et => et.ExpenseType)
                .OrderByDescending(p => p.ExpenseId);
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await _context.Expenses.FindAsync(expenseId);
        }
    }
}
