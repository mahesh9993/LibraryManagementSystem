using ApplicationCore.Models;

namespace LibraryManagementAPI.Services
{
    public interface IBorrowerService
    {
        Task<IEnumerable<Borrower>> GetAllAsync();
        Task<Borrower?> GetByIdAsync(int id);
    }
}
