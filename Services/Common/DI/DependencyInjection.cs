using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services.Common.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }
}