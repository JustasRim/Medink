using api.Application.Common.Interfaces;
using Application.Helpers;
using Application.Services;
using Infrastructure.Helpers;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("MedinkDatabase"),
            builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        services.AddScoped<IApplicationDbContext>(q => q.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IMedicService, MedicService>();
        services.AddTransient<IPatientService, PatientService>();
        services.AddTransient<ISymptomService, SymptomService>();
        services.AddTransient<IAuthService, AuthService>();

        services.AddSingleton<IAuthHelper, AuthHelper>();

        return services;
    }
} 
