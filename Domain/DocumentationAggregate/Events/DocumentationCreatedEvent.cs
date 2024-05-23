using Domain.Common.Abstractions;

namespace Domain.DocumentationAggregate.Events
{
	public record DocumentationCreatedEvent(Documentation Documentation) : IDomainEvent;
}
