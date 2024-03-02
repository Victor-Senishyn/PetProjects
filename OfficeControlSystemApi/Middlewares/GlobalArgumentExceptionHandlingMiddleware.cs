using System.Net;

namespace OfficeControlSystemApi.Middlewares
{
    public class GlobalArgumentExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public GlobalArgumentExceptionHandlingMiddleware(
            ILogger<GlobalArgumentExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
