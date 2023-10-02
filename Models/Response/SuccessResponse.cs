using MovieTicketBookingBe.Models.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace MovieTicketBookingBe.Models.Response
{
    public class SuccessResponse : ApiResponse
    {
        [JsonProperty("data")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object Data { get; set; }

        [JsonProperty("pagination")]
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PaginationDTO? Pagination { get; set; }

        public SuccessResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Data = null;
            Pagination = null;
        }

        public SuccessResponse(HttpStatusCode statusCode, string message, object? data = null) : base(statusCode, message)
        {
            Data = data;
            Pagination = null;
        }

        public SuccessResponse(HttpStatusCode statusCode, string message, object? data = null, PaginationDTO? pagination = null) : base(statusCode, message)
        {
            Data = data;
            Pagination = pagination;
        }
    }
}
