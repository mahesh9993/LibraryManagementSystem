namespace LibraryManagementAPI.Models
{
    public class ReservationDetailInputModel
    {
        public string? UserNumber { get; set; }
        public string? BookCopyNumber { get; set; }
        public int CreatedBy { get; set; }
    }
}
