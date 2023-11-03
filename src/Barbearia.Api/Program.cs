// dotnet ef --startup-project ../Barbearia.Api/ migrations add InitialMigration --context PersonContext
// dotnet ef --startup-project ../Barbearia.Api/ database update --context PersonContext

using Elmah.Io.Extensions.Logging;
using Barbearia.Persistence.Extensions;
using Barbearia.Application.Extensions;
using Barbearia.Api.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //config porta padrão
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenLocalhost(5000);
        });

        // Add services to the container.
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Logging.AddElmahIo(options =>
        {
            options.ApiKey = "d8c818e18e154af08ffa697802b4687a";
            options.LogId = new Guid("92f71801-82ae-469e-b6ed-e4c9cc6cb221");
        });

        builder.Logging.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseElmahIoExtensionsLogging();

        app.UseAuthorization();

        app.MapControllers();

        //app.ResetDatabaseAsync();

        app.Run();
    }
}