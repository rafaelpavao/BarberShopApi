using Barbearia.Application.Contracts.Repositories;
using Barbearia.Persistence.DbContexts;
using Barbearia.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.Persistence.Extensions;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositoryServices();
        services.AddDbContexts(configuration);

        return services;
    }

    private static void AddRepositoryServices(this IServiceCollection services)
    {        
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"))
                .EnableSensitiveDataLogging()
        );
        services.AddDbContext<ItemContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"))
                .EnableSensitiveDataLogging()
        );
        services.AddDbContext<OrderContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"))
                .EnableSensitiveDataLogging()
        );
    }
}