using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IReservationService
    {
        Task<CommonResponse> ReserveBooks(ReservationDetailInputModel reservationDetailInputModel);
        Task<CommonResponse> GetReserveBooks(ReservationDetailInputModel reservationDetailInput);
    }
}
