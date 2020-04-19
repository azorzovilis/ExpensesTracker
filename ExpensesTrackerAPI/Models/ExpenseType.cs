namespace ExpensesTrackerAPI.Models
{
    using Enums;

    public class ExpenseType
    {
        public ExpenseTypeId ExpenseTypeId { get; set; }

        public string Description { get; set; }
    }
}