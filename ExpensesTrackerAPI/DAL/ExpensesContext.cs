namespace ExpensesTrackerAPI.DAL
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ExpensesContext : DbContext
    {
        public ExpensesContext(DbContextOptions<ExpensesContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>().ToTable("Expense");

            modelBuilder.Entity<Expense>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,4)");
        }
    }
}
