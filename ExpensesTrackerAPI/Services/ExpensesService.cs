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
        private readonly IDataRepository<Expense> _expensesRepository;
        private readonly IDataRepository<ExpenseType> _expenseTypeRepository;
        private readonly IMemoryCacheService<IEnumerable<ExpenseType>> _cacheService;

        public ExpensesService(IDataRepository<Expense> expensesRepository,
            IDataRepository<ExpenseType> expenseTypeRepository,
            IMemoryCacheService<IEnumerable<ExpenseType>> cacheService)
        {
            _expensesRepository = expensesRepository;
            _expenseTypeRepository = expenseTypeRepository;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await _expensesRepository.Get(
                    orderBy: q => q.OrderByDescending(exp => exp.ExpenseId),
                    includeProperties: exp => exp.ExpenseType)
                .ToListAsync();
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await _expensesRepository.GetById(expenseId, includeProperties: exp => exp.ExpenseType);
        }

        public async Task<Expense> CreateExpense(Expense expense)
        {
            _expensesRepository.Add(expense);
            return await _expensesRepository.SaveAsync(expense);
        }

        public async Task<Expense> UpdateExpense(Expense expense)
        {
            _expensesRepository.Update(expense);

            return await _expensesRepository.SaveAsync(expense);
        }

        public async Task<Expense> DeleteExpense(Expense expense)
        {
            _expensesRepository.Delete(expense);
            return await _expensesRepository.SaveAsync(expense);
        }

        public async Task<bool> ExpenseExists(int expenseId)
        {
            return await _expensesRepository.EntityExists(expenseId);
        }

        public async Task<IEnumerable<ExpenseType>> GetExpenseTypes()
        {
            const string expenseTypeKey = "ExpenseTypes";

            return await _cacheService
                .GetOrCreate(expenseTypeKey, async () => await _expenseTypeRepository.Get().ToListAsync());
        }
    }
}