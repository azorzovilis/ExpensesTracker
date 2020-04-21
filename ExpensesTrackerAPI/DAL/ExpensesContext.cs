namespace ExpensesTrackerAPI.DAL
{
    using System;
    using System.Linq;
    using ExpensesTrackerAPI.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ExpensesContext : DbContext
    {
        public ExpensesContext()
        { }

        public ExpensesContext(DbContextOptions<ExpensesContext> options)
            : base(options)
        { }

        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseType> ExpenseType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>().ToTable("Expense");
            modelBuilder.Entity<ExpenseType>().ToTable("ExpenseType");

            modelBuilder.Entity<Expense>()
                .Property(p => p.Amount).HasColumnType("decimal(18,4)");

            modelBuilder.Entity<ExpenseType>().HasData(
                Enum.GetValues(typeof(ExpenseTypeId))
                    .Cast<ExpenseTypeId>()
                    .Select(e => new ExpenseType
                    {
                        ExpenseTypeId = e,
                        Description = e.ToString()
                    })
            );

            modelBuilder.Entity<Expense>().HasData(
                new Expense { ExpenseId = 1, Amount = 10.4m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Alex", TransactionDate = new DateTime(2020,01, 17) },
                new Expense { ExpenseId = 2, Amount = 5.6m, Currency = "USD", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Eliza", TransactionDate = new DateTime(2020, 04, 19) },
                new Expense { ExpenseId = 3, Amount = 35.88m, Currency = "EUR", ExpenseTypeId = ExpenseTypeId.Other, Recipient = "Artemis", TransactionDate = new DateTime(2020, 04, 20) },
                new Expense { ExpenseId = 4, Amount = 105.22m, Currency = "CHF", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Thomas", TransactionDate = new DateTime(2020, 04, 21) }
            );
        }
    }
}
