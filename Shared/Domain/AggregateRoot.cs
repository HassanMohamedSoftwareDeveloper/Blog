namespace Shared.Abstractions.Domain;

public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>
{
    public TKey Id { get; set; }
    protected List<IDomainEvent> _events = new();
    public IEnumerable<IDomainEvent> Events => _events;
    protected virtual void AddEvent(IDomainEvent @event) => _events.Add(@event);
    public void ClearEvents() => _events.Clear();
}
