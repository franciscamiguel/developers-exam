using Domain.Events;
using MediatR;

namespace Infrastructure.Events;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
        => DomainEvent = domainEvent;

    public TDomainEvent DomainEvent { get; }
}