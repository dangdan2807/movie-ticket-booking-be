using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.ViewModels
{
    public class PaginationVM
    {
        [Range(1, int.MaxValue, ErrorMessage = "Current page must be greater than or equal to 1")]
        [DefaultValue(1)]
        public int currentPage { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than or equal to 1")]
        [DefaultValue(10)]
        public int pageSize { get; set; } = 10;

        [RegularExpression("^(ASC|DESC)$", ErrorMessage = "Sort is invalid")]
        [DefaultValue("ASC")]
        public string sort { get; set; } = "ASC";
    }
}
