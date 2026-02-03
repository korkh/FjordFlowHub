namespace Contracts
{
    // Using record for immutability and built-in serialization support
    public record FreightDeleted
    {
        public string Id { get; init; }
    }
}
