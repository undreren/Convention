using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Convention.WebApi.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (NullReferenceException e)
            {
                // React to specific exceptions in separate exception blocks
                throw;
            }
        }
    }
}
