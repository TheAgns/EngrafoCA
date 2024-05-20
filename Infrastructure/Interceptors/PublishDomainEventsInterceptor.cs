using Domain.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors
{
	public class PublishDomainEventInterceptor : SaveChangesInterceptor
	{
		private readonly IPublisher _publisher;

        public PublishDomainEventInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }
        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
		{
			await PublishDomainEvents(eventData.Context);
			return await base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		private async Task PublishDomainEvents(DbContext? dbContext)
		{
			if (dbContext is null) 
			{
				return;
			}

			// Get entities and domain events
			var entities = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
				.Where(entry => entry.Entity.DomainEvents.Any())
				.Select(entry => entry.Entity)
				.ToList();

			var domainEvents = entities.SelectMany(entry => entry.DomainEvents).ToList();

			// Clears the domainEvent list of all the entities
			entities.ForEach(entity => entity.ClearDomainEvents());

			// Then we publish the domainEvents that we extracted
			foreach ( var domainEvent in domainEvents)
			{
				await _publisher.Publish(domainEvent);
			}

		}
	}
}
