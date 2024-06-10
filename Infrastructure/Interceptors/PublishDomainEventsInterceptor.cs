using Domain.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;

namespace Infrastructure.Interceptors
{
	public class PublishDomainEventsInterceptor : SaveChangesInterceptor
	{
		private readonly IPublisher _publisher;
		private readonly ILogger _logger;

        public PublishDomainEventsInterceptor(IPublisher publisher, ILogger logger)
        {
            _publisher = publisher;
			_logger = logger;
        }

		// Automatically publishes domain events upon calling the saveChangesAsync method
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
			foreach (var domainEvent in domainEvents)
			{
				var eventType = typeof(IDomainEvent).Assembly.GetName().Name;
				_logger.Information("Publishing Domain Event: {eventType}", eventType);
				await _publisher.Publish(domainEvent);
			}

		}
	}
}
