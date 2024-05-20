using ErrorOr;

namespace Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Documentation
        {
            public static Error DocumentationNotFound =>
                Error.NotFound(
                    code: "Documentation.NotFound",
                    description: "Documentation(s) could not be found.");

			public static Error DocumentationNotCreated =>
				Error.NotFound(
					code: "Documentation.NotCreated",
					description: "The documentation could not be created.");
		}
    }
}
