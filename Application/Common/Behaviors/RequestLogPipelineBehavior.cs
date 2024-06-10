using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Common.Behaviors
{
	public class RequestLogPipelineBehavior<TRequest, TResponse> :
		IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
		where TResponse : IErrorOr
	{
		// Encapsulates the validation logic of the command/query (request)
		private readonly ILogger<RequestLogPipelineBehavior<TRequest, TResponse>> _logger;

		public RequestLogPipelineBehavior(ILogger<RequestLogPipelineBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		// If there are no validation errors, we invoke the handler

		/* If there are validation errors, we convert the validationRes to a list of ErrorOr
		   and return them.
		 */
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			// If no validator is defined for the command/query
			// just invoke the handler
			if (_logger is null)
			{
				return await next();
			}

			string requestName = typeof(TRequest).Name;

			_logger.LogInformation("Processing Request: {requestName}", requestName);

			var response = await next();

			
			if (response.IsError)
			{
				var errorName = response.Errors.First().Code;

				using (LogContext.PushProperty("Error", response.Errors.First(), true))
					_logger.LogError("Completed Request: {requestName}, with error: {errorName}", requestName, errorName);
			}

			if (!response.IsError)
			{
				_logger.LogInformation("Completed Request: {requestName}", requestName);
			}			

			// This invokes the handler
			return response;
		}
	}
}
