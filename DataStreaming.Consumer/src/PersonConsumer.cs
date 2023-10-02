using MassTransit;
using System.Text.Json;

namespace DataStreaming.Consumer;

public class PersonConsumer : IConsumer<PersonMessage>
{
    public async Task Consume(ConsumeContext<PersonMessage> context)
    {
        var message = context.Message;

        PersonDataStreamingSpecification specification = new(message);

        if (specification.IsInsert)
            Console.WriteLine(@$"Inserted a new person: 
                {JsonSerializer.Serialize(message.Payload.After)}
            ");

        else if (specification.IsUpdate)
            Console.WriteLine(@$"Updated a person: 
                Before: {JsonSerializer.Serialize(message.Payload.Before)}
                After: {JsonSerializer.Serialize(message.Payload.After)}
            ");

        else if (specification.IsDelete)
            Console.WriteLine(@$"Deleted a person: 
                Deleted person: {JsonSerializer.Serialize(message.Payload.Before)}
            ");

        await Task.FromResult(() => Console.WriteLine("Message consumed. Here we can put a code to replicate data to another service"));
    }
}
