namespace LibraryManagementAPI.Models
{
    public class LoanDetailModel
    {
        public int UserID { get; set; }
        public int LoanCount { get; set; }
        public int ReservationCount { get; set; }
        public bool HasOverdue { get; set; }
    }
}
