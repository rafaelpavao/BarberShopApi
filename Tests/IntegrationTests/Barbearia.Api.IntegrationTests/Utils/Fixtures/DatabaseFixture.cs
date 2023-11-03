using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Api.IntegrationTests.Utils.Fixtures;

[CollectionDefinition(nameof(DatabaseCollection))]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture> { }

public class DatabaseFixture : IDisposable
{
    private readonly PersonContext _personContext;
    private readonly OrderContext _orderContext;
    private readonly ItemContext _itemContext;
    public readonly string DatabaseName = $"BarbeariaTests-{Guid.NewGuid()}";
    public readonly string ConnectionString;
    private bool _disposed;

    public DatabaseFixture()
    {
        ConnectionString = $"Host=localhost;Database={DatabaseName};Username=postgres;Password=1973;Include Error Detail=true";
        var builderPerson = new DbContextOptionsBuilder<PersonContext>();
        var builderOrder = new DbContextOptionsBuilder<OrderContext>();
        var builderItem = new DbContextOptionsBuilder<ItemContext>();
        builderPerson.UseNpgsql(ConnectionString);
        builderOrder.UseNpgsql(ConnectionString);
        builderItem.UseNpgsql(ConnectionString);

        _personContext = new PersonContext(builderPerson.Options);
        _personContext.Database.Migrate();

        _orderContext = new OrderContext(builderOrder.Options);
        _orderContext.Database.Migrate();

        _itemContext = new ItemContext(builderItem.Options);
        _itemContext.Database.Migrate();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _personContext.Database.EnsureDeleted();
                _orderContext.Database.EnsureDeleted();
                _itemContext.Database.EnsureDeleted();
            }
            _disposed = true;
        }
    }

}