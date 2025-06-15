using LibraryManagementAPI.Infrastructure.Services;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService loanService;
        public LoanController(ILoanService loanService)
        {
            this.loanService = loanService;
        }

        [HttpGet]
        [Route("CheckBookAvailability")]
        public async Task<IActionResult> CheckBookAvailability(int bookCopyID)
        {
            var result = await loanService.CheckBookAvailability(bookCopyID);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLoanDetailsByUser")]
        public async Task<IActionResult> GetLoanDetailsByUser(int userID)
        {
            var result = await loanService.GetLoanDetailsByUser(userID);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveLoan")]
        public async Task<IActionResult> SaveLoan(LoanModel loan)
        {
            var result = await loanService.SaveLoan(loan);
            return Ok(result);
        }
    }
}
