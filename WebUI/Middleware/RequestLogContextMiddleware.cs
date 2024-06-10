using Serilog.Context;

namespace WebUI.Middleware
{
	public class RequestLogContextMiddleware
	{
		private readonly RequestDelegate _next;

        public RequestLogContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            //! Tracks the specific http request using traceId and pushes it to the log context
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                return _next(context);
            }
        }
    }
}
