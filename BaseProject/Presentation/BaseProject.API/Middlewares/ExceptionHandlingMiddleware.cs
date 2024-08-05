using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using SendGrid.Helpers.Errors.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;
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
            using (LogContext.PushProperty("RemoteIpAddress", httpContext.Connection.RemoteIpAddress))
            using (LogContext.PushProperty("UserId", httpContext.User?.Identity?.IsAuthenticated != null || true ? httpContext.User.Identity.Name : "-"))
            {
                try
                {
                    await next(httpContext);
                }
                catch (Exception exception) when (httpContext.RequestAborted.IsCancellationRequested)
                {
                    const string message = "Request was cancelled";
                    _logger.LogWarning(exception, message);

                    httpContext.Response.Clear();
                    httpContext.Response.StatusCode = StatusCodes.Status499ClientClosedRequest;
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(httpContext, ex);
                }
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

            var details = CreateExceptionDetails(httpContext, exception);
            var json = JsonSerializer.Serialize(details);
            _logger.LogError(exception, exception.Message, json);
            await httpContext.Response.WriteAsync(json);
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
        private ProblemDetails CreateExceptionDetails(in HttpContext context, in Exception exception)
        {
            var statusCode = context.Response.StatusCode;
            var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);


            if (string.IsNullOrEmpty(reasonPhrase))
            {
                reasonPhrase = DefaultErrorMessage;
            }

            var problemDetails = new ProblemDetails()
            {
                Title = reasonPhrase,
                Status = statusCode,
                Detail = exception.Message,
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = context.Request.Path,
                Extensions = new Dictionary<string, object>()
            };

            if (_env.IsProduction())
            {
                return problemDetails;
            }

            if(context.Request.QueryString.HasValue)
                problemDetails.Extensions["queryString"] = context.Request.QueryString.Value;
            if (context.Request.HasFormContentType)
                problemDetails.Extensions["form"] = context.Request.Form.ToDictionary(k => k.Key, v => v.Value.ToString());

            problemDetails.Extensions["clientIp"] = context.Connection.RemoteIpAddress.ToString();
            problemDetails.Extensions["traceId"] = context.TraceIdentifier;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            problemDetails.Extensions["data"] = exception.Data;
            return problemDetails;
        }
    }
}

