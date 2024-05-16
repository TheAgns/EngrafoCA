namespace Domain.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>> 
        where TId : notnull
	{
		public TId Id { get; protected set; }

        protected BaseEntity(TId id)
        {
            Id = id;
        }
        protected BaseEntity()
        {

        }
        public override bool Equals(object? obj)
        {
            return obj is BaseEntity<TId> entity && Id.Equals(entity.Id);
        }

        public bool Equals(BaseEntity<TId>? other)
        {
            return Equals((object?)other);
        }

        public static bool operator ==(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
		
	
}
