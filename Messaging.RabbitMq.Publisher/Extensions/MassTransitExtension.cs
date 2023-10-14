﻿using System;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Messaging.RabbitMq.Publisher.Extensions
{
    public static class MassTransitExtension
    {
        public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                // O transporte a utilizar podendo ser Amazon SQS ou Azure Service Bus, Kafka e etc..
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), host =>
                    {
                        host.Username("user");
                        host.Password("password");
                    });

                    //Recomenda-se que seja o ultimo a ser chamado
                    config.ConfigureEndpoints(context);
                });
            });

            //Configurações de inicialização ou paragem do Bus
            services.AddOptions<MassTransitHostOptions>().Configure(options =>
            {
                options.WaitUntilStarted = false;
            });

            return services;
        }

        public static IApplicationBuilder AddMassTransitConfiguration(this IApplicationBuilder app)
        {
            //Implementação do HealthCheck
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready")
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions());
            });

            return app;
        }
    }
}

