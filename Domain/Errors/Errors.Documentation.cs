using ErrorOr;

namespace Domain.Errors
{
    public static partial class Errors
    {
        public static class Documentation
        {
            public static Error DocumentationNotFound =>
                Error.NotFound(
                    code: "Documentation.NotFound",
                    description: "Documentation could not be found.");
        }
    }
}
