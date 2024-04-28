using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
	public class DocumentationTemplateHeading : BaseEntity
	{
		public string Title { get; set; }
		public bool Hide { get; set; }
		public int Position { get; set; }

		public DocumentationTemplate DocumentationTemplate { get; set; }
		public int DocumentationTemplateId { get; set; }

	}
}
