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
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql("Server=localhost;port=6543;user id=postgres;password=toor;database=CoffeeMachine;");
    
    public virtual DbSet<Banknote> Banknotes { get; set; }
    public virtual DbSet<BanknotesMachine> BanknotesMachines { get; set; }
    public virtual DbSet<Machine> Machines { get; set; }
    public virtual DbSet<Transaction> Transactions { get; set; }
    public virtual DbSet<Coffee> Coffees { get; set; }
    public virtual DbSet<Purchase> Purchases { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coffee_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
            entity.Property(e=> e.Price).IsRequired();
        });

        modelBuilder.Entity<Banknote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("money_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Par).IsRequired();
        });
        
        modelBuilder.Entity<BanknotesMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banknotes_machines_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CountBanknotes).IsRequired();
            entity.HasOne(e => e.Machine)
                .WithMany(e => e.BanknotesMachines)
                .HasForeignKey(e => e.IdMachine)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("banknotes_machines_machine_fk");
            entity.HasOne(e => e.Banknote)
                .WithMany(e => e.BanknotesMachines)
                .HasForeignKey(e => e.IdBanknote)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("banknotes_machines_banknote_fk");
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("machine_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(30);
        });
        
        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.HasOne(e => e.Coffee)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.IdCoffee).OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_coffee_fk");
            entity.HasOne(e => e.Machine)
                .WithMany(e => e.Purchases)
                .HasForeignKey(e => e.IdMachine)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("purchase_machine_fk");
        });
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CountBanknotes).IsRequired();
            entity.Property(e => e.Type).IsRequired();
            entity.HasOne(e => e.Banknote)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.IdBanknote)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_banknote_fk");
            entity.HasOne(e => e.Purchase)
                .WithMany(e => e.Transactions)
                .HasForeignKey(e => e.IdPurchase)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_purchase_fk");
        });
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}