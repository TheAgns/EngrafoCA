using Domain.Common;
using Domain.Entities;

namespace Domain.ValueObjects
{
    public class DocumentationFavorite : ValueObject
    {

        public int UserId { get; set; }

        public int DocumentationId { get; set; }

		protected override IEnumerable<object> GetAtomicValues()
		{
			yield return UserId;
			yield return DocumentationId;
		}
	}
}
