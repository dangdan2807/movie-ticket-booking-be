namespace MovieTicketBookingBe.Models.DTO
{
    public class ShortUrlDTO
    {
        public string hashId { get; set; }
        public string longUrl { get; set; }
        public string shortUrl { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updateAt { get; set; }
        public bool status { get; set; }
        public int userId { get; set; }
        public ulong clickCount { get; set; }
    }
}
