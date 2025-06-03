using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("SaveUser")]
        public async Task<IActionResult> SaveUserDetails([FromBody] UserDetailInputModel userDetailInput)
        {
            var success = await userService.SaveUserDetails(userDetailInput);
            return Ok(success);
        }

        [HttpGet("GetUserDetail")]
        public async Task<IActionResult> GetUserDetail(string userNumber)
        {
            var success = await userService.GetUserDetail(userNumber);
            return Ok(success);
        }

    }
}
