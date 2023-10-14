namespace MovieTicketBookingBe.Models.DTO
{
    public class GetShortUrlsDTO
    {
        public ICollection<ShortUrlDTO> shortUrls { get; set; }
        public PaginationDTO pagination { get; set; }
    }
}
