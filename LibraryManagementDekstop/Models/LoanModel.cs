namespace LibraryManagementDekstop.Models
{
    public class LoanModel
    {
        public string UserNumber { get; set; }
        public string BookCopyNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
