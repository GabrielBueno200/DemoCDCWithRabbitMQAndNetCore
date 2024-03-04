using System.Net.Mime;
using DataStreaming.Consumer;
using MassTransit;
using Microsoft.Extensions.Hosting;

const string rabbitMqHost = "localhost";
const int rabbitMqPort = 5672;
const string rabbitMqUsername = "guest";
const string rabbitMqPassword = "guest";
const string rabbitMqvHost = "vhost";
const string rabbitMqQueueName = "person-cdc-queue";

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(configuration =>
        {
            configuration.AddConsumer<PersonConsumer>();

            configuration.UsingRabbitMq((context, configuration) =>
            {
                configuration.Host(new Uri($"rabbitmq://{rabbitMqHost}:{rabbitMqPort}/{rabbitMqvHost}"), host =>
                {
                    host.Username(rabbitMqUsername);
                    host.Password(rabbitMqPassword);
                });

                configuration.ReceiveEndpoint(rabbitMqQueueName, endpoint =>
                {
                    endpoint.ConfigureConsumeTopology = false;

                    endpoint.DefaultContentType = new ContentType("application/json");
                    endpoint.UseRawJsonDeserializer();

                    endpoint.UseMessageRetry(retryConfig => retryConfig.Interval(2, 100));
                    endpoint.Consumer<PersonConsumer>();
                });

            });
        });
    })
    .RunConsoleAsync();
