using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Services
{
    public interface ILoanService
    {
        Task<CommonResponse> GetLoanDetailsByUser(string userNumber);
        Task<CommonResponse> CheckBookAvailability(string bookCopyNumber);
        Task<CommonResponse> SaveLoan(LoanModel loan);
        Task<CommonResponse> GetLoansByUser(BookReturnModel model);
        Task<CommonResponse> ReturnBookDelete(BookReturnModel model);
    }
}
