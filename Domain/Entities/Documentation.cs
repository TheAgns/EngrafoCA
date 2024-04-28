using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Documentation : BaseEntity
	{
		public string Name { get; private set; }
		public string OwnerId { get; private set; }
		public int? Version { get; private set; }
		public DocumentationTemplate DocumentationTemplate { get; private set; }
		public int DocumentationTemplateId { get; private set; }
		public DocumentationCategory DocumentationCategory { get; private set; }
		public int DocumentationCategoryId { get; private set; }
		public int? OriginalVersion { get; private set; }
		public string? VersionCreatedById { get; private set; }
		public DateTime? VersionCreatedDateTime { get; private set; }
		public bool Hide {  get; private set; }
		public bool ReadOnly { get; private set; }

	}
}
