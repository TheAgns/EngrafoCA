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
                    description: "Documentation with the given id could not be found.");
        }
    }
}
