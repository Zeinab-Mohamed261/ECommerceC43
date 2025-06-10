using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    class CacheAttribute(int DurationInSec = 90) :ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Create cache key
            string cacheKey = CreateCacheKey(context.HttpContext.Request);

            //search for value with cache key
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue =await cacheService.GetAsync(cacheKey);

            //return value if is not null
            if (cacheValue is not null)
            {
                context.Result = new ContentResult
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;  //exit from function
            }

            //return if is null
            var ExecutedContent = await next.Invoke(); //call next action

            //set value with cache key
            if(ExecutedContent.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var query in request.Query.OrderBy(q => q.Key))
            {
                Key.Append($"{query.Key}={query.Value}&");
            }
            return Key.ToString();
        }
    }
}
