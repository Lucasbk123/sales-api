using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly  ILogger<ValidationExceptionMiddleware> _logger;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleKeyNotFoundExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request");
                throw;
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };


            return context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonSerializerOptions));
        }


        private static Task HandleKeyNotFoundExceptionAsync(HttpContext context, KeyNotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new ApiResponse
            {
                Success = false,
                Message = exception.Message
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonSerializerOptions));
        }
    }
}
