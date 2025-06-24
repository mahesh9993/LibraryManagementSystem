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
        Task<CommonResponse> GetLoansByUser(LoanModel model);
        Task<CommonResponse> ReturnBookDelete(LoanModel model);
    }
}
