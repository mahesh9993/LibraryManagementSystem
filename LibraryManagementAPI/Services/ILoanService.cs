using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface ILoanService
    {
        Task<CommonResponse> GetLoanDetailsByUser(int userID);
        Task<CommonResponse> CheckBookAvailability(int bookcopyID);
        Task<CommonResponse> SaveLoan(LoanModel loan);
    }
}
