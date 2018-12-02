using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xcelerator.Api.Model;

namespace Xcelerator.Api.Configurations.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context,ILogger logger)
        {
            try
            {
                // must be awaited
                await _next(context);
            }
            catch (Exception ex)
            {
                logger.LogError("Unhandled exception ...", ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            //TODO:Mapping if (exception is NotFoundExe) code = HttpStatusCode.NotFound;
            if (exception is ArgumentNullException) code = HttpStatusCode.BadRequest;
            else if (exception is HttpRequestException) code = HttpStatusCode.BadRequest;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;


            return WriteExceptionAsync(context, exception, code);
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            return response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new ExceptionResponse
                {
                    Code = (int)code,
                    Message = exception.Message,
                    Exception = exception.GetType().Name
                }
            }));
        }
    }
}
