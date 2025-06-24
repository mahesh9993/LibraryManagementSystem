namespace LibraryManagementAPI.Models
{
    public class LoanModel
    {
        public int UserID { get; set; }
        public int BookCopyID { get; set; }
        public string UserNumber { get; set; }
        public string BookCopyNumber { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
