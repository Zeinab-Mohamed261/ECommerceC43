using Azure.Core;
using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace ECommerceC43.Api
{
    public class CustomExceptionHandlerMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandlerMiddlware(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddlware> logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var badRequestResponse = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException =>  GetBadRequestException(badRequestException , badRequestResponse),
                _ => StatusCodes.Status500InternalServerError,//default
            };

            //set status code for response
            //httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //set content typr for response
            //httpContext.Response.ContentType = "application/json";

            //response object
            var Response = new ErrorToReturn
            {

                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message,
            };

            //return object as json
            // var ResponseToReturn = JsonSerializer.Serialize(Response);
            //await httpContext.Response.WriteAsync(ResponseToReturn);
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestException(BadRequestException badRequestException, ErrorToReturn? response)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point : {httpContext.Request.Path} is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
