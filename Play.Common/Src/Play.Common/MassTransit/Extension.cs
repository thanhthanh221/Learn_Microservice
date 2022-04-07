using System.Reflection;
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
                });
            });
            services.AddMassTransitHostedService();
            return services;
        }
    }
}