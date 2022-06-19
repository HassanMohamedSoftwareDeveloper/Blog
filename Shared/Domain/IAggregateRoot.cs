namespace Shared.Abstractions.Domain;

public interface IAggregateRoot<TKey>
{
    public TKey Id { get; set; }
    public IEnumerable<IDomainEvent> Events { get; }
}
