using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Services
{
    public interface IBookService
    {
        Task<CommonResponse> RegisterBook(Book book);
        Task<CommonResponse> GetAllBooks();
    }
}
