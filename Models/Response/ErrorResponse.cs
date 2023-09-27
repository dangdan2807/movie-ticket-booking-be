using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace MovieTicketBookingBe.Models.Response
{
    public class ErrorResponse : ApiResponse
    {
        [JsonProperty("errors")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Errors { get; set; }

        public ErrorResponse(HttpStatusCode statusCode, string message, object? errors = null) : base(statusCode, message)
        {
            this.Errors = errors;
        }
    }
}
