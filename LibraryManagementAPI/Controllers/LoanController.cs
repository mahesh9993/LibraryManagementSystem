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
        public async Task<IActionResult> CheckBookAvailability(string bookCopyNumber)
        {
            var result = await loanService.CheckBookAvailability(bookCopyNumber);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLoanDetailsByUser")]
        public async Task<IActionResult> GetLoanDetailsByUser(string userNumber)
        {
            var result = await loanService.GetLoanDetailsByUser(userNumber);
            return Ok(result);
        }

        [HttpPost]
        [Route("SaveLoan")]
        public async Task<IActionResult> SaveLoan(LoanModel loan)
        {
            var result = await loanService.SaveLoan(loan);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetLoansByUser")]
        public async Task<IActionResult> GetLoansByUser(LoanModel model)
        {
            var result = await loanService.GetLoansByUser(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("ReturnBookDelete")]
        public async Task<IActionResult> ReturnBookDelete(LoanModel model)
        {
            var result = await loanService.ReturnBookDelete(model);
            return Ok(result);
        }
    }
}
