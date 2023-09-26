using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class ErrorResponse : ApiResponse
    {
        public object errors { get; set; }

        public ErrorResponse(HttpStatusCode statusCode, string message, object errors) : base(statusCode, message)
        {
            this.errors = errors;
        }
    }
}
