namespace DataStreaming.Consumer;

public record PersonMessage(Payload Payload);

public record Payload(Person Before, Person After);

public record Person(int Id, string Name, string Address, string Phone);