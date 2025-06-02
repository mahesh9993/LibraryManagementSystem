using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reserveService;

        public ReservationController(IReservationService reserveService)
        {
            this.reserveService = reserveService;
        }

        [HttpPost("ReserveBooks")]
        public async Task<IActionResult> ReserveBooks(string userNumber, string bookNumber,int CreatedBy)
        {
            var success = await reserveService.ReserveBooks(userNumber, bookNumber,CreatedBy);
            return Ok(success);
        }

        [HttpGet("GetReserveBooks")]
        public async Task<IActionResult> GetReserveBooks(string? userNumber, string? bookNumber)
        {
            var success = await reserveService.GetReserveBooks(userNumber, bookNumber);
            return Ok(success);
        }
    }
}
