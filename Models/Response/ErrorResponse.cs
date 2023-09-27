using Newtonsoft.Json;
using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class ErrorResponse : ApiResponse
    {
        [JsonProperty("errors")]
        public object Errors { get; set; }

        public ErrorResponse(HttpStatusCode statusCode, string message, object? errors = null) : base(statusCode, message)
        {
            this.Errors = errors;
        }
    }
}
