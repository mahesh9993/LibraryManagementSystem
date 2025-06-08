using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IBookUpdateRepository
    {
        Task<int> UpdateBooks(BookDetailModel model);
    }
}
