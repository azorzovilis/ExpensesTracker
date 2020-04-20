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

            modelBuilder
                .Entity<ExpenseType>().HasData(
                    Enum.GetValues(typeof(ExpenseTypeId))
                        .Cast<ExpenseTypeId>()
                        .Select(e => new ExpenseType
                        {
                            ExpenseTypeId = e,
                            Description = e.ToString()
                        })
                );
        }
    }
}
