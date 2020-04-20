namespace ExpensesTrackerAPI.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;
    using Enums;

    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public string Recipient { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public ExpenseTypeId ExpenseTypeId { get; set; }
        
        public virtual ExpenseType ExpenseType { get; set; }
    }
}
