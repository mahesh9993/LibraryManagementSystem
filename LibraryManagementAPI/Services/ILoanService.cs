using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Services
{
    public interface ILoanService
    {
        Task<CommonResponse> GetLoanDetailsByUser(int userID);
        Task<CommonResponse> CheckBookAvailability(int bookcopyID);
        Task<CommonResponse> SaveLoan(LoanModel loan);
        Task<CommonResponse> GetLoansByUser(LoanModel model);
        Task<CommonResponse> ReturnBookDelete(LoanModel model);
    }
}
