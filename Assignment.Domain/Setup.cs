using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Domain;

public static class Setup
{
    public static void ConfigureDomain(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IMeetingService, MeetingService>();
        services.AddTransient<IJwtService, JwtService>();
    }
}