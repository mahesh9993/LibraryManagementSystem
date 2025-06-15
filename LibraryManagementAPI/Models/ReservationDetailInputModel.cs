namespace LibraryManagementAPI.Models
{
    public class ReservationDetailInputModel
    {
        public string? UserNumber { get; set; }
        public int? BookNumber { get; set; }
        public int CreatedBy { get; set; }
    }
}
