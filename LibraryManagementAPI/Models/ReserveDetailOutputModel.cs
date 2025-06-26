namespace LibraryManagementAPI.Models
{
    public class ReserveDetailOutputModel
    {
        public string? UserNumber { get; set; }
        public int? BookCopyID { get; set; }
        public string BookCopyNumber { get; set; }
        public string? Title { get; set; }
        public DateTime ReserveDate { get; set; }
    }
}
