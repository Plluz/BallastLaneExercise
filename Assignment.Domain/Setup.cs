using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Domain;

public static class Setup
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IMeetingService, MeetingService>();
    }
}