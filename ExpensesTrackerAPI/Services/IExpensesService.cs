namespace ExpensesTrackerAPI.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExpensesTrackerAPI.Models;
    using Models;

    public interface IExpensesService
    {
        IEnumerable<Expense> GetExpenses();

        Task<Expense> GetExpense(int expenseId);

        Task<Expense> CreateExpense(Expense expense);

        Task<Expense> UpdateExpense(Expense expense);

        Task<Expense> DeleteExpense(Expense expense);

        Task<bool> ExpenseExists(int expenseId);

        Task<IEnumerable<ExpenseType>> GetExpenseTypes();
    }
}
