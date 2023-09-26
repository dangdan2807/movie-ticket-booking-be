using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class SuccessResponse : ApiResponse
    {
        public object data { get; set; }

        public SuccessResponse(HttpStatusCode statusCode, string message, object data) : base(statusCode, message)
        {
            this.data = data;
        }
    }
}
