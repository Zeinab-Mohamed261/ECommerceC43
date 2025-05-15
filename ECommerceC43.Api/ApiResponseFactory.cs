using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace ECommerceC43.Api
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
                   .Select(M => new ValidationError
                   {
                       Field = M.Key,
                       Errors = M.Value.Errors.Select(e => e.ErrorMessage)
                   });
            var Response = new ValidationErrorToReturn
            {
                ValidationErrors = Errors,
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
