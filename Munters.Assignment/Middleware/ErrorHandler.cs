using System.Net;

namespace Munters.Assignment.Middleware
{
    /// <summary>
    /// This is global error handler, it will catch errors from any place inside the application
    /// </summary>
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Here I would log the information like exception stack+exception message+inner exception's details if any
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = ex switch
                {
                    ApplicationException _ => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException _ => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
