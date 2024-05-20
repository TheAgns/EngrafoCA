using ErrorOr;

namespace Domain.Common.Errors
{
	public static partial class Errors
	{
		public static class DocumentationTemplate
		{
			public static Error TemplateNotFound =>
				Error.NotFound(
					code: "Template.NotFound",
					description: "Template(s) could not be found.");
		}
	}
}
