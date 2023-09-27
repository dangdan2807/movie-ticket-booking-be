using Newtonsoft.Json;
using System.Net;

namespace MovieTicketBookingBe.Models.Response
{
    public class ApiResponse
    {
        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }
        [JsonProperty("Message")]
        public string? Message { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message) {
            this.StatusCode = statusCode;
            this.Message = message;
        }
    }
}
