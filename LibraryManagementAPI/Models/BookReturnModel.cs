namespace LibraryManagementAPI.Models
{
    public class BookReturnModel
    {
        public string? UserNumber { get; set; }
        public int BookCopyID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
