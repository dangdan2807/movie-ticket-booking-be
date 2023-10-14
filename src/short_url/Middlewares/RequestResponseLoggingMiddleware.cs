using System.Text;

namespace MovieTicketBookingBe.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly Serilog.ILogger _logger;
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var builderRequest = new StringBuilder();
            var request = await FormatRequest(context.Request);
            builderRequest.AppendLine("Request: " + request);
            builderRequest.AppendLine("Request headers: ");
            foreach (var header in context.Request.Headers)
            {
                builderRequest.Append(header.Key).Append(':').Append(header.Value + ", ");
            }
            _logger.Information(builderRequest.ToString());

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await _next(context);

            //Format the response from the server
            var builderResponse = new StringBuilder();
            var response = await FormatResponse(context.Response);
            builderResponse.Append("Response: ").AppendLine(response);

            _logger.Information(builderResponse.ToString());
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            var formattedRequest = $"{request.Method} {request.Path}{request.QueryString} \nRequest Body: {body}";
            request.Body.Position = 0;
            return formattedRequest;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);
            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"\nStatus code: {response.StatusCode}\nResponse body: {text}";
        }
    }
}
