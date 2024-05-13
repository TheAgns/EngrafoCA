using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
	/* Defines that the pipeline intercepts a request (command/query)
	   and that request returns a response of type ErrorOr (if interceptet)
	   ic case of a validation error.
	 */
	public class ValidationBehavior<TRequest, TResponse> : 
		IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : IErrorOr
	{
		// Encapsulates the validation logic of the command/query (request)
		private readonly IValidator<TRequest>? _validator;

		public ValidationBehavior(IValidator<TRequest>? validator = null)
		{
			_validator = validator;
		}

		// If there are no validation errors, we invoke the handler

		/* If there are validation errors, we convert the validationRes to a list of ErrorOr
		   and return them.
		 */
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			// If no validator is defined for the command/query
			// just invoke the handler
			if (_validator is null)
			{
				return await next();
			}

			var validationRes = await _validator.ValidateAsync(request, cancellationToken);

			if (!validationRes.IsValid)
			{
				// Converts the FluentValidation type ValidationFailure to
				// an Error.Validation object from the ErrorOr nuGet Package
				var errors = validationRes.Errors
					.ConvertAll(validationFailure => Error.Validation(
						validationFailure.PropertyName, 
						validationFailure.ErrorMessage));

				/* (dynamic) checks during runtime if it is possible to convert
				   the ValidationFailure object into a list of ErroOr,
				   otherwise throws a runtime exception.
				   Generally not good to use, but in this case we know
				   that it is always possible because of the constraint on
				   the TResponse type.
				*/
				return (dynamic)errors;
			}


			// This invokes the handler
			return await next();
		}
	}
}
