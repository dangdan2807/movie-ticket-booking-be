namespace MovieTicketBookingBe.src.Models.DTO
{
    public class GetRolesDTO
    {
        public ICollection<RoleDTO> roles { get; set; }
        public PaginationDTO pagination { get; set; }
    }
}
