namespace Domain.Common.Abstractions
{
    public abstract class AggregateRoot<TId> : BaseEntity<TId> where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id)
        {

        }

        protected AggregateRoot()
        {

        }

    }
}
