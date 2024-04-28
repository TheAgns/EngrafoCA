using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Documentation.Commands.CreateDocumentation
{
    public class CreateDocumentationDto
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public int DocumentationTemplateId { get; set; }
        public int DocumentationCategoryId { get; set; }
        public bool Hide { get; set; }
        public bool ReadOnly { get; set; }
    }
}
