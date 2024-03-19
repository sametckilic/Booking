using System.Data;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Booking.Infrastructure.Exceptions;
using Booking.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Identity.Client;

namespace Booking.WebAPI.Extensions
{
    /// <summary>
    /// middleware for handling global exceptions in the application.
    /// </summary>
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = MediaTypeNames.Application.Json;
                var responseModel = ResponseModel<string>.Error(response.StatusCode, ex.ToString());

                if(ex is DatabaseValidationException)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.StatusCode = response.StatusCode;
                    responseModel.Message = ex.Message;
                    logger.LogError($"{DateTime.UtcNow} - database validation exception occured! Details: {ex.Message}");
                }

                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.StatusCode = response.StatusCode;
                    logger.LogError($"{DateTime.UtcNow} {ex.ToString}");
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }

        }
    }
}
