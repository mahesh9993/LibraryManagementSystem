using LibraryManagementAPI.Models;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Services
{
    public interface IUserService
    {
        Task<bool> SaveUserDetails(UserDetailInputModel userDetailInput);
        Task<UserDetailOutputModel?> GetUserDetail(string userNumber);
    }
}
