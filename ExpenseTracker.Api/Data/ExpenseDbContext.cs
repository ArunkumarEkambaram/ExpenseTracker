using ExpenseTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Data
{
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(c => c.CategoryName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Expense>(o =>
            {
                o.HasKey(o => o.Id);
                o.Property(x => x.Amount).HasColumnType("numeric(10,2)");
                o.Property(x => x.Title).IsRequired().HasMaxLength(100);
                o.Property(x => x.Notes).IsRequired(false).HasMaxLength(200);
            });

            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, CategoryName = "Grocery" },
                    new Category { Id = 2, CategoryName = "Fuel" },
                    new Category { Id = 3, CategoryName = "Shopping" },
                    new Category { Id = 4, CategoryName = "Entertainment" },
                    new Category { Id = 5, CategoryName = "Bills & Utilities" },
                    new Category { Id = 6, CategoryName = "Healthcare" },
                    new Category { Id = 7, CategoryName = "Education" },
                    new Category { Id = 8, CategoryName = "Travel" },
                    new Category { Id = 9, CategoryName = "Personal Care" },
                    new Category { Id = 10, CategoryName = "General" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
