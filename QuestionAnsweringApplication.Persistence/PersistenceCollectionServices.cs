using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuestionAnsweringApplication.Persistence;

public static class PersistenceCollectionServices
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext's
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            _ = options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration.GetConnectionString("AppConnection"));
        });

        services.AddTransient<IRepository, Repository>();
        services.AddScoped<IApplicationDbContextInitializer, ApplicationDbContextInitializer>();
    }
}
