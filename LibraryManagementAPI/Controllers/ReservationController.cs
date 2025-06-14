﻿using LibraryManagementAPI.Models;
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
        public async Task<IActionResult> ReserveBooks(ReservationDetailInputModel reservationDetailInput)
        {
            var success = await reserveService.ReserveBooks(reservationDetailInput);
            return Ok(success);
        }

        [HttpGet("GetReserveBooks")]
        public async Task<IActionResult> GetReserveBooks()
        {
            var success = await reserveService.GetReserveBooks();
            return Ok(success);
        }

        [HttpPost("UpdateReservationDetailByBookCopyID")]
        public async Task<IActionResult> UpdateReservationDetail(UpdateReserveModel inputModel)
        {
            var success = await reserveService.UpdateReservationDetail(inputModel);
            return Ok(success);
        }
    }
}
