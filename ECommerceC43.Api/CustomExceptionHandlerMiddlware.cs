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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex , "Error Happend");

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,//default
                };

                //set status code for response
                //httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                //httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                //set content typr for response
                httpContext.Response.ContentType = "application/json";

                //response object
                var Response = new ErrorToReturn
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return object as json
                   // var ResponseToReturn = JsonSerializer.Serialize(Response);
                   //await httpContext.Response.WriteAsync(ResponseToReturn);
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
