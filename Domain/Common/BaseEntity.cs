namespace Domain.Common
{
	public abstract class BaseEntity
	{
		public Guid Id { get; private init; } = Guid.NewGuid();
        public DateTime Created { get; private init; } = DateTime.Now;
        public string? CreatedBy { get; private init; }

        public DateTimeOffset LastModified { get; set; } = DateTimeOffset.Now;

        public string? LastModifiedBy { get; set; }

    }
		
	
}
