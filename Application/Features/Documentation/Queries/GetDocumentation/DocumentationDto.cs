using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Domain.Entities;

namespace Application.Features.Documentation.Queries.GetDocumentation
{
    public class DocumentationDto : BaseDto
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        public int? Version { get; set; }
        // TODO - Category
        public int DocumentationTemplateId { get; set; }
        public int DocumentationCategoryId { get; set; }
        public int? OriginalVersion { get; set; }
        public string? VersionCreatedById { get; set; }
        public DateTime? VersionCreatedDateTime { get; set; }

    }
}
