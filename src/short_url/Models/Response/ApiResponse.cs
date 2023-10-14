using Newtonsoft.Json;
using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class ApiResponse
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode statusCode { get; set; }

        [JsonProperty("Message")]
        public string? message { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message)
        {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}
