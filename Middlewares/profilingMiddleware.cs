using System.Diagnostics;

namespace MyBGList.Middlewares
{
    public class profilingMiddleware(RequestDelegate _next, ILogger<profilingMiddleware> logger)
    {
       
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            logger.LogInformation(
            "Request {Method} {Path} executed in {ElapsedMilliseconds} ms",
            context.Request.Method,
            context.Request.Path,
            stopwatch.ElapsedMilliseconds);

        }
    }
}
