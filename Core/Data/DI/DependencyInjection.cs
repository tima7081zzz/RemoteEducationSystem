using Data.Interfaces;
using Data.Interfaces.Repositories;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {
        services
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IProfessorRepository, ProfessorRepository>()
            .AddTransient<IStudentRepository, StudentRepository>()
            .AddTransient<ISubjectRepository, SubjectRepository>();

        return services;
    }
}