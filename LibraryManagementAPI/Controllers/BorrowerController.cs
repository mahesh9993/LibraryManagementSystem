using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowerController : ControllerBase
    {
        private readonly IBorrowerService borrowerService;

        public BorrowerController(IBorrowerService borrowerService)
        {
            this.borrowerService = borrowerService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var students = await borrowerService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await borrowerService.GetByIdAsync(id);
            if (student == null)
                return NotFound();
            return Ok(student);
        }
    }
}
