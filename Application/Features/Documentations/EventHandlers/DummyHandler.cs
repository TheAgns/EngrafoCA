using Domain.DocumentationAggregate.Events;
using MediatR;

namespace Application.Features.Documentations.EventHandlers
{
	public class DummyHandler : INotificationHandler<DocumentationCreatedEvent>
	{
		public Task Handle(DocumentationCreatedEvent notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
