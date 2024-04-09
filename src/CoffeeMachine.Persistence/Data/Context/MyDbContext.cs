using CoffeeMachine.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Persistence.Data.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    { }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    { }
    
    public virtual DbSet<Coffee> Coffees { get; set; }
    public virtual DbSet<Money> Monies { get; set; }
    public virtual DbSet<Machine> Machines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coffee_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
            entity.Property(e=> e.Price).IsRequired();
        });

        modelBuilder.Entity<Money>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("money_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Amount).IsRequired();
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("machine_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(30);
        });
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}