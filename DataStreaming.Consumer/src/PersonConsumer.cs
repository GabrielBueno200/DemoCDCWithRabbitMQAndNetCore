using MassTransit;
using System.Text.Json;

namespace DataStreaming.Consumer;

public class PersonConsumer : IConsumer<PersonMessage>
{
    public async Task Consume(ConsumeContext<PersonMessage> context)
    {
        var message = context.Message;

        if (message.IsInsert)
            Console.WriteLine(@$"Inserted a new person: 
                {JsonSerializer.Serialize(message.Payload.After)}
            ");

        if (message.IsUpdate)
            Console.WriteLine(@$"Updated a person: 
                Before: {JsonSerializer.Serialize(message.Payload.Before)}
                After: {JsonSerializer.Serialize(message.Payload.After)}
            ");

        if (message.IsDelete)
            Console.WriteLine(@$"Deleted a person: 
                Deleted person: {JsonSerializer.Serialize(message.Payload.Before)}
            ");

        await Task.FromResult(() => Console.WriteLine("Message consumed. Here we can put some logic to traffic data to another target system"));
    }
}
