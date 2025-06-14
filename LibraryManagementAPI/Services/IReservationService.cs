using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IReservationService
    {
        Task<CommonResponse> ReserveBooks(string userNumber, string bookNumber, int CreatedBy);
        Task<CommonResponse> GetReserveBooks(string userNumber, string bookNumber);
    }
}
