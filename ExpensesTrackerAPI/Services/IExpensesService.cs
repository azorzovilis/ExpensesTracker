namespace ExpensesTrackerAPI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IExpensesService
    {
        IEnumerable<Expense> GetExpenses();
        Task<Expense> GetExpense(int expenseId);
    }
}
