using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
	public class DocumentationTemplate : BaseEntity
	{
		public string Name { get; set; }
		public bool Hide { get; set; }
		public IList<DocumentationTemplateHeading> TemplateHeadings { get; private set; }
    }
}
