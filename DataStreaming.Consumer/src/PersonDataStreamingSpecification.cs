namespace DataStreaming.Consumer;

public class PersonDataStreamingSpecification(PersonMessage _message)
{
    public bool IsDelete => _message.Payload.After is null && _message.Payload.Before is not null;

    public bool IsUpdate => _message.Payload.After is not null && _message.Payload.Before is not null;

    public bool IsInsert => _message.Payload.After is not null && _message.Payload.Before is null;
}