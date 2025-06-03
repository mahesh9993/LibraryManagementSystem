using LibraryManagementAPI.Infrastructure.Services;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpPost]
        [Route("RegisterBook")]
        public async Task<IActionResult> RegisterBook(Book book)
        {
            var result = await bookService.RegisterBook(book);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var students = await bookService.GetAllBooks();
            return Ok(students);
        }
    }
}
