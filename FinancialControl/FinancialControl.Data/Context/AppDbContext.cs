using FinancialControl.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Revenue> Revenues { get; set; }

    /// <summary>
    /// Usando fluente API para popular o base e criar validações para campos como obrigatórios
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Revenue>().HasKey(x => x.Id);
        modelBuilder.Entity<Revenue>().Property(x => x.Description).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Revenue>().Property(x => x.Value).HasPrecision(20, 2).IsRequired();
        modelBuilder.Entity<Revenue>().Property(x => x.Date).IsRequired();

        modelBuilder.Entity<Expense>().HasKey(x => x.Id);
        modelBuilder.Entity<Expense>().Property(x => x.Description).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Expense>().Property(x => x.Value).HasPrecision(20, 2).IsRequired();
        modelBuilder.Entity<Expense>().Property(x => x.Date).IsRequired();

        modelBuilder.Entity<Revenue>().HasData(new Revenue
        {
            Id = 1,
            Description = "Salário",
            Value = 3000,
            Date = new DateTime(),
        });

        modelBuilder.Entity<Revenue>().HasData(new Revenue
        {
            Id = 2,
            Description = "Salário bônus",
            Value = 3000,
            Date = new DateTime(),
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 1,
            Description = "Mensalidade facul",
            Value = 700,
            Date = new DateTime(),
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 2,
            Description = "Internet",
            Value = 70,
            Date = new DateTime(),
        });
    }
}
