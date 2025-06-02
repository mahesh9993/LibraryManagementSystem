using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IReservationService
    {
        Task<bool> ReserveBooks(string userNumber, string bookNumber, int CreatedBy);
        Task<ReserveDetailOutputModel?> GetReserveBooks(string? userNumber, string? bookNumber);
    }
}
