namespace ExpensesTrackerAPI.Extensions
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Expense>().ToTable("Expense");
            builder.Entity<ExpenseType>().ToTable("ExpenseType");

            builder.Entity<Expense>()
                .Property(p => p.Amount).HasColumnType("decimal(18,4)");

            builder.Entity<ExpenseType>().HasData(
                Enum.GetValues(typeof(ExpenseTypeId))
                    .Cast<ExpenseTypeId>()
                    .Select(e => new ExpenseType
                    {
                        ExpenseTypeId = e,
                        Description = e.ToString()
                    })
            );

            builder.Entity<Expense>().HasData(
                new Expense { ExpenseId = 1, Amount = 10.4m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Alex", TransactionDate = new DateTime(2020, 01, 17) },
                new Expense { ExpenseId = 2, Amount = 5.6m, Currency = "CHF", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Eliza", TransactionDate = new DateTime(2020, 04, 19) },
                new Expense { ExpenseId = 3, Amount = 35.88m, Currency = "EUR", ExpenseTypeId = ExpenseTypeId.Other, Recipient = "Artemis", TransactionDate = new DateTime(2020, 04, 20) },
                new Expense { ExpenseId = 4, Amount = 105.22m, Currency = "GBP", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Thomas", TransactionDate = new DateTime(2020, 04, 21) },
                new Expense { ExpenseId = 5, Amount = 44.17m, Currency = "JPY", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "John", TransactionDate = new DateTime(2020, 04, 22) },
                new Expense { ExpenseId = 6, Amount = 25.00m, Currency = "CHF", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "Rick", TransactionDate = new DateTime(2020, 04, 22) },
                new Expense { ExpenseId = 7, Amount = 182.59m, Currency = "AUD", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Patricia", TransactionDate = new DateTime(2020, 04, 23) },
                new Expense { ExpenseId = 8, Amount = 56.16m, Currency = "EUR", ExpenseTypeId = ExpenseTypeId.Other, Recipient = "Glen", TransactionDate = new DateTime(2020, 04, 23) },
                new Expense { ExpenseId = 9, Amount = 8.25m, Currency = "EUR", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Maria", TransactionDate = new DateTime(2020, 04, 23) },
                new Expense { ExpenseId = 10, Amount = 98.99m, Currency = "CHF", ExpenseTypeId = ExpenseTypeId.Drinks, Recipient = "George", TransactionDate = new DateTime(2020, 04, 24) },
                new Expense { ExpenseId = 11, Amount = 3.99m, Currency = "USD", ExpenseTypeId = ExpenseTypeId.Food, Recipient = "Paul", TransactionDate = new DateTime(2020, 04, 24) },
                new Expense { ExpenseId = 12, Amount = 54.12m, Currency = "EUR", ExpenseTypeId = ExpenseTypeId.Other, Recipient = "Caren", TransactionDate = new DateTime(2020, 04, 24) }
             );
        }
    }
}