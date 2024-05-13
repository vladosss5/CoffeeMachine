using System.Data.Common;
using CoffeeMachine.Core.Models;
using CoffeeMachine.Persistence.Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.UnitTests;

public class Data : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<DataContext> _contextOptions;
    private readonly DataContext _context;

    public Data()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();    
        
        _contextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(_connection)
            .Options;
        
        using var context = new DataContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"CREATE VIEW AllResources AS SELECT Url FROM CoffeeMachine;";
            viewCommand.ExecuteNonQuery();
        }
        
        context.AddRangeAsync(
            new Coffee{Id = 1, Name = "Cappuccino", Price = 500},
            new Coffee{Id = 2, Name = "Latte", Price = 400},
            new Coffee{Id = 3, Name = "Espresso", Price = 300},
            new Banknote{Id = 4, Nominal = 500},
            new Banknote{Id = 2, Nominal = 100},
            new Banknote{Id = 3, Nominal = 50},
            new Machine{Id = 3, SerialNumber = "11", Description = "wdw"},
            new BanknoteToMachine
            {
                Id = 1,
                Banknote = new Banknote{Id = 4},
                Machine = new Machine{Id = 1, SerialNumber = "11", Description = "wdw"}
            }
        );
        
        context.SaveChangesAsync();
    }
    public DataContext CreateContext() => new DataContext(_contextOptions);
    
    public void Dispose() => _connection.Dispose();
}