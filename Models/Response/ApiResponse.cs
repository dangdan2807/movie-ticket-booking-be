using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class ApiResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public string? message { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message) {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}
