using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Convention.WebApi.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate next;

        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("INFO: Log request attempt");
            
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"ERROR: Log error details");
                throw;
            }

            Console.WriteLine("INFO: Log request success");
        }
    }
}
