using ApplicationCore.Common;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Services
{
    public interface IBookService
    {
        Task<CommonResponse> RegisterBook(Book book);
        Task<CommonResponse> GetAllBooks();
        Task<CommonResponse> GetAllBookCategories();
        Task<CommonResponse> UpdateBook(BookDetailModel book);
    }
}
