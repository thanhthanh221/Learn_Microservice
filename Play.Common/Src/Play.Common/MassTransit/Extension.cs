using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using MassTransit.MultiBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Play.Common.Settings;

namespace Play.Common.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(Assembly.GetEntryAssembly());
                configure.UsingRabbitMq((context, configurator) =>
                {
                    IConfiguration? Configuration = context.GetService<IConfiguration>();
                    ServiceSettings? serviceSettings = Configuration?.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

                    var rabbitMQSettings =  Configuration?.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings?.Host);
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings?.   ServiceName, false));
                    configurator.UseMessageRetry(entryConfigurator =>
                    {
                        entryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                    });
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}