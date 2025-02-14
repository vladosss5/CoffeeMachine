using CoffeeMachine.Application.Exceptions;
using CoffeeMachine.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CoffeeMachine.Persistence.Data.Context;

/// <summary>
/// Контекст для работы с базой данных.
/// </summary>
public partial class DataContext : DbContext
{
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public DataContext()
    { }

    /// <summary>
    /// Перегрузка конструктора класса.
    /// </summary>
    /// <param name="options"></param>
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }
    
    /// <summary>
    /// Банкноты.
    /// </summary>
    public virtual DbSet<Banknote> Banknotes { get; set; }
    
    /// <summary>
    /// Банкноты в кофемашине.
    /// </summary>
    public virtual DbSet<BanknoteToMachine> BanknotesToMachines { get; set; }
    
    /// <summary>
    /// Кофе.
    /// </summary>
    public virtual DbSet<Coffee> Coffees { get; set; }
    
    /// <summary>
    /// Кофе в кофемашине.
    /// </summary>
    public virtual DbSet<CoffeeToMachine> CoffeesToMachines { get; set; }
    
    /// <summary>
    /// Кофемашина.
    /// </summary>
    public virtual DbSet<Machine> Machines { get; set; }
    
    /// <summary>
    /// Заказ.
    /// </summary>
    public virtual DbSet<Order> Orders { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="Transactions"/>
    /// </summary>
    public virtual DbSet<Transaction> Transactions { get; set; }
    
    /// <summary>
    /// Пользователь.
    /// </summary>
    public virtual DbSet<User> Users { get; set; }
    
    /// <summary>
    /// <inheritdoc cref="Role"/>
    /// </summary>
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banknote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("money_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Nominal).IsRequired();
        });
        
        modelBuilder.Entity<BanknoteToMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banknotes_machines_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.HasOne(e => e.Machine)
                .WithMany(e => e.BanknotesToMachines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("banknotes_machines_machine_fk");
            entity.HasOne(e => e.Banknote)
                .WithMany(e => e.BanknotesToMachines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("banknotes_machines_banknote_fk");
        });
        
        modelBuilder.Entity<Coffee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coffee_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
            entity.Property(e=> e.Price).IsRequired();
        });
        
        modelBuilder.Entity<CoffeeToMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coffee_in_machine_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.HasOne(e => e.Machine)
                .WithMany(e => e.CoffeesToMachines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("coffee_in_machine_machine_fk");
            entity.HasOne(e => e.Coffee)
                .WithMany(e => e.CoffeesToMachines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("coffee_in_machine_coffee_fk");
        });
        
        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("machine_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(30);
        });
        
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.HasOne(e => e.Coffee)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("order_coffee_fk")
                .IsRequired();
            entity.HasOne(e => e.Machine)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("order_machine_fk")
                .IsRequired();
        });
        
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.IsPayment).IsRequired();
            entity.HasOne(e => e.Banknote)
                .WithMany(e => e.Transactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_banknote_fk");
            entity.HasOne(e => e.Order)
                .WithMany(e => e.Transactions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("transaction_order_fk");
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Login).IsRequired().HasMaxLength(30);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(30);
            entity.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_role_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");
            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
        });
    }

    /// <summary>
    /// Метод сохранения изменений в базе данных.
    /// </summary>
    public async Task TrySaveChangesToDbAsync()
    {
        try
        {
            await SaveChangesAsync();
        }
        catch (NotFoundException e)
        { 
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}