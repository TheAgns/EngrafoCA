namespace Domain.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>> 
        where TId : notnull
	{
		public TId Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public string? CreatedBy { get; protected set; }

        public DateTimeOffset LastModified { get; protected set; }

        public string? LastModifiedBy { get; protected set; }

        protected BaseEntity(TId id)
        {
            Id = id;
            Created = DateTime.Now;
            LastModified = DateTimeOffset.Now;
        }
        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(BaseEntity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b) =>
            (a == b);

        public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b) =>
            !(a == b);

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
		
	
}
