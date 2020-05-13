namespace ExpensesTrackerAPI.DAL
{
    using Extensions;
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

            modelBuilder.Seed();
        }
    }
}
