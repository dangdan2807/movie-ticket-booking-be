using MovieTicketBookingBe.src.Models.Response;
using System.Net;
using System.Text.Json;

namespace MovieTicketBookingBe.src.Middlewares
{
    public class CustomErrorResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Serilog.ILogger _logger;

        public CustomErrorResponseMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory, Serilog.ILogger logger)
        {
            _next = next;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 403)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json"; // Đặt kiểu content là JSON (tuỳ chọn)

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(
                    HttpStatusCode.Forbidden,
                    "Access is denied"
                )));
            }
            else if (context.Response.StatusCode == 401)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json"; // Đặt kiểu content là JSON (tuỳ chọn)

                await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse(
                    HttpStatusCode.Unauthorized,
                    "Access is denied"
                )));
            }
        }
    }
}
