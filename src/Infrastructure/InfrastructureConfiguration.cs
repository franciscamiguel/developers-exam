using Domain.Interfaces;
using Infrastructure.Data.Contexts;
using Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLConnection"), b => b.MigrationsAssembly(typeof(SqlDbContext).Assembly.FullName)));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<IDomainEventHandler, DomainEventHandler>();

        services.AddScoped<SqlDbContext>();


        return services;
    }
}