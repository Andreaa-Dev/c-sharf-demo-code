using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user.src.Utils;

namespace user.src.Middleware
{
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandler> _logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                // in case no error => go next logic
                await _next(context);
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"Error");

                // Log the exception
                // optional 
                // _logger.LogError(ex, "An unhandled exception has occurred.");

                // Handle the exception and return a response
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    ex.StatusCode,
                    ex.Message,
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}