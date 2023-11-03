using Barbearia.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Barbearia.Api.Extensions;

internal static class StartupHelperExtensions
{
    public static void ResetDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                var customerContext = scope.ServiceProvider.GetService<PersonContext>();
                var orderContext = scope.ServiceProvider.GetService<OrderContext>();
                var itemContext = scope.ServiceProvider.GetService<ItemContext>();

                if (customerContext != null)
                {
                    customerContext.Database.EnsureDeletedAsync();
                    customerContext.Database.Migrate();
                }
                if (orderContext != null)
                {
                    orderContext.Database.Migrate();
                }
                if (itemContext != null)
                {
                    itemContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao atualizar o banco de dados: {ex.Message}");
            }
        }
    }
}