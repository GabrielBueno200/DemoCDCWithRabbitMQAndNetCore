namespace DataStreaming.Consumer;

public record PersonMessage(Payload Payload)
{
    public bool IsDelete => Payload.After is null && Payload.Before is not null;

    public bool IsUpdate => Payload.After is not null && Payload.Before is not null;

    public bool IsInsert => Payload.After is not null && Payload.Before is null;
};

public record Payload(Person Before, Person After);

public record Person(int Id, string Name, string Address, string Phone);