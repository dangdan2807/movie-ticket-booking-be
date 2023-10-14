using MovieTicketBookingBe.Models.Response;
using System.Net;
using System.Text.Json;

namespace MovieTicketBookingBe.Middlewares
{
    public class CustomErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public CustomErrorResponseMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            var statusCode = context.Response.StatusCode;
            if (statusCode == 403)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json"; // Đặt kiểu content là JSON (tuỳ chọn)
                context.Response.StatusCode = StatusCodes.Status403Forbidden;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(
                    HttpStatusCode.Forbidden,
                    "Access is denied"
                )));
            }
            else if (statusCode == 401)
            {
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Access is denied"
                )));
            }
            else if (statusCode == 400)
            {
                var message = context.Response.Headers["Message"];
                context.Response.Clear();
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(
                    HttpStatusCode.BadRequest,
                    message
                )));
            }
        }
    }
}
