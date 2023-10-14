namespace MovieTicketBookingBe.Models.DTO
{
    public class GetUsersDTO
    {
        public ICollection<UserDTO> users { get; set; }
        public PaginationDTO pagination { get; set; }
    }
}
