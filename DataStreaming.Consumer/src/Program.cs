using System.Net.Mime;
using DataStreaming.Consumer;
using MassTransit;
using Microsoft.Extensions.Hosting;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                var rabbitMqHost = "localhost";
                var rabbitMqPort = 5672;
                var rabbitMqUsername = "admin";
                var rabbitMqPassword = "lalala";
                var rabbitMqvHost = "vhost";
                var rabbitMqQueueName = "dbo.Person";

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{rabbitMqHost}:{rabbitMqPort}/{rabbitMqvHost}"), h =>
                    {
                        h.Username(rabbitMqUsername);
                        h.Password(rabbitMqPassword);
                    });

                    cfg.ReceiveEndpoint(rabbitMqQueueName, e =>
                    {
                        e.ConfigureConsumeTopology = false;
                        
                        e.DefaultContentType = new ContentType("application/json");
                        e.UseRawJsonDeserializer();

                        e.UseMessageRetry(retryConfig => retryConfig.Interval(2, 100));
                        e.Consumer<PersonConsumer>();
                    });

                });
            });
        });

using var host = CreateHostBuilder(args).Build();

host.Run();