using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace user.src.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        // must have constructor to pass request to next function
        // constructor
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            // go to next logic 
            _next = next;
            // log information
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Console.WriteLine("logging");

            // log/print => ILogger 
            // print information of request: method, path

            // log the incoming request
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            // measure how long request take
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            // Log the outgoing response
            _logger.LogInformation($"Outgoing response: {context.Response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");
        }
    }
}