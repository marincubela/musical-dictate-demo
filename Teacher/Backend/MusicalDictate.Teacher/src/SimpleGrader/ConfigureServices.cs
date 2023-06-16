using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleGrader.Interfaces;
using SimpleGrader.Services;

namespace SimpleGrader;

public static class ConfigureServices
{
    public static IServiceCollection AddSimpleGraderServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IWebApiClient, WebApiClient>();

        services.AddMassTransit(x =>
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"]);
                    h.Password(configuration["RabbitMq:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}