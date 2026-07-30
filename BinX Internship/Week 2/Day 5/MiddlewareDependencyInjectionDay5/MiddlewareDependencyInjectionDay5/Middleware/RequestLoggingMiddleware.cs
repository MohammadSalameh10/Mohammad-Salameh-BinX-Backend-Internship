namespace MiddlewareDependencyInjectionDay5.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("----- Incoming Request -----");
            Console.WriteLine($"Method: {context.Request.Method}");
            Console.WriteLine($"Path: {context.Request.Path}");

            Endpoint? endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                Console.WriteLine("Endpoint: Not selected yet");
            }
            else
            {
                Console.WriteLine($"Endpoint: {endpoint.DisplayName}");
            }

            Console.WriteLine("----------------------------");

            await _next(context);
        }
    }
}
