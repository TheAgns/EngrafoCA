namespace Domain.Common.Abstractions
{
	public interface IHasDomainEvents
	{
		public IReadOnlyList<IDomainEvent> DomainEvents { get; }

		public void ClearDomainEvents();
	}
}
