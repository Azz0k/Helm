using Helm.Core.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Helm.Api
{
    public sealed class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ForbiddenException exception)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Type = "AuthorizationFailure",
                    Title = "Forbidden",
                    Detail = "You are not allowed to access"
                };
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (ValidationException exception)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Type = "ValidationFailure",
                    Title = "Validation error",
                    Detail = "One or more validation errors has occurred"
                };

                if (exception.Errors is not null)
                {
                    problemDetails.Extensions["errors"] = exception.Errors;
                }

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
