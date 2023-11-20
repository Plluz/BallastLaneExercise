using Assignment.Domain.Interfaces.Repositories;
using Assignment.Infrastructure.Data;
using Assignment.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Assignment.Infrastructure;

public static class Setup
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMeetingRepository, MeetingRepository>();

        services.AddSingleton<IHostedService, DatabaseSeeder>();
        services.AddHostedService<DatabaseSeeder>();
        services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));
    }
}