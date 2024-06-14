using Domain.Events;

namespace Domain.Interfaces;

public interface IHasDomainEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
}