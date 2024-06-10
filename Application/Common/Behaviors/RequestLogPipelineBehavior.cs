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
		private readonly ILogger<RequestLogPipelineBehavior<TRequest, TResponse>> _logger;

		public RequestLogPipelineBehavior(ILogger<RequestLogPipelineBehavior<TRequest, TResponse>> logger)
		{
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
						if (_logger is null)
			{
				return await next();
			}

			string requestName = typeof(TRequest).Name;

			_logger.LogInformation("Processing Request: {requestName}", requestName);

			var response = await next();

			
			if (response.IsError)
			{
				using (LogContext.PushProperty("Error", response.Errors.First(), true))
					_logger.LogError("Completed Request: {requestName} with error", requestName);
			}

			_logger.LogInformation("Completed Request: {requestName}", requestName);

			// This invokes the handler
			return response;
		}
	}
}
