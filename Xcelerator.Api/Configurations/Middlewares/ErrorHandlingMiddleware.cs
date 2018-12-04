using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Xcelerator.Model.ErrorHandler;

namespace Xcelerator.Api.Configurations.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            return WriteExceptionAsync(context, exception);
        }

        private Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception.ToString());

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            return exception is CustomException
                ? response.WriteAsync(exception.ToString())
                : response.WriteAsync(exception.Message);
        }
    }
}
