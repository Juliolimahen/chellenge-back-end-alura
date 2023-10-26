using FinancialControl.Core.Models;
using FinancialControl.Core.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinancialControl.Data.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Revenue> Revenues { get; set; }

    // Outras configurações e métodos OnModelCreating aqui...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Chame a implementação base

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
            Date = DateTime.Now,
        });

        modelBuilder.Entity<Revenue>().HasData(new Revenue
        {
            Id = 2,
            Description = "Salário bônus",
            Value = 3000,
            Date = DateTime.Now,
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 1,
            Description = "Mensalidade facul",
            Value = 700,
            Date = DateTime.Now,
        });

        modelBuilder.Entity<Expense>().HasData(new Expense
        {
            Id = 2,
            Description = "Internet",
            Value = 70,
            Date = DateTime.Now,
        });

        // Mais configurações do Identity, se necessário...
    }
}
