namespace LibraryManagementDekstop.Models
{
    public class ReserveDetailOutputModel
    {
        public string? UserNumber { get; set; }
        public int? BookNumber { get; set; }
        public string? Title { get; set; }
        public DateTime ReserveDate { get; set; }
    }
}
