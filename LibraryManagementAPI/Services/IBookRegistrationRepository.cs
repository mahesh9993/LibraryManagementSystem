using LibraryManagementAPI.Models;

namespace LibraryManagementAPI.Services
{
    public interface IBookRegistrationRepository
    {
        Task<int> SaveBooks(Book model);
    }
}
