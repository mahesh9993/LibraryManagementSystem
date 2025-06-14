using LibraryManagementAPI.Common;
using LibraryManagementAPI.Models;
using System.Threading.Tasks;

namespace LibraryManagementAPI.Services
{
    public interface IUserService
    {
        Task<CommonResponse> SaveUserDetails(UserDetailInputModel userDetailInput);
        Task<CommonResponse> GetUserDetail(string userNumber);
    }
}
