using BaseProject.Domain.Utils;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using SendGrid.Helpers.Errors.Model;
namespace BaseProject.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private const string DefaultErrorMessage = "An error occurred while processing your request";

        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception) when (httpContext.RequestAborted.IsCancellationRequested)
            {
                const string message = "Request was cancelled";
                _logger.LogDebug(exception, message);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

            var details = CreateExceptionDetails(httpContext, exception);
            var json = JsonSerializer.Serialize(details);
            await httpContext.Response.WriteAsync(json);
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                //ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        private ExceptionHandlingModel CreateExceptionDetails(in HttpContext context, in Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);

            //if (exception.GetType() == typeof(ValidationException))
            //{
            //    ((ValidationException)exception).Errors
            //}

                if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = DefaultErrorMessage;
            }

            var problemDetails = new ExceptionHandlingModel()
            {
                Status = statusCode,
                Title = reasonPhrase,   
                Message = exception.Message,
                Url = context.Request.Path,
                Extensions =
                {
                    ["errorCode"] = Guid.NewGuid().ToString()
                }
            };

            if (_env.IsProduction())
            {
                return problemDetails;
            }
            //problemDetails.Detail = exception.ToString();
            problemDetails.Instance = exception.Source;
            problemDetails.Extensions["clientIp"]= context.Connection.RemoteIpAddress.ToString();
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["data"] = exception.Data;
            return problemDetails;
        }
    }
}

