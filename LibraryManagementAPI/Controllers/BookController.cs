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

        [HttpPost]
        [Route("UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookDetailModel book)
        {
            var result = await bookService.UpdateBook(book);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var students = await bookService.GetAllBooks();
            return Ok(students);
        }

        [HttpGet]
        [Route("GetAllBookCategories")]
        public async Task<IActionResult> GetAllBookCategories()
        {
            var students = await bookService.GetAllBookCategories();
            return Ok(students);
        }

        [HttpPost]
        [Route("UpdateBookCopyStatus")]
        public async Task<IActionResult> UpdateBookCopyStatus(int bookCopyID)
        {
            var result = await bookService.UpdateBookCopyStatus(bookCopyID);
            return Ok(result);
        }
    }
}
