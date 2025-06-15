namespace LibraryManagementAPI.Models
{
    public class LoanModel
    {
        public int UserID { get; set; }
        public int BookCopyID { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
