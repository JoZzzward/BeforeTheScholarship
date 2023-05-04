namespace BeforeTheScholarship.Api.Middlewares
{
    public class DefaultPageMiddleware
    {
        private readonly RequestDelegate _next;

        public DefaultPageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("api/index.html");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
