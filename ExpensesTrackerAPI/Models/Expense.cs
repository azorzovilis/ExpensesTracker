namespace ExpensesTrackerAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public ExpenseType ExpenseType { get; set; }
    }
}
