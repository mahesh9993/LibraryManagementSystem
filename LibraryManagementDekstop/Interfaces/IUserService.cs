using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Interfaces
{
    public interface IUserService
    {
        Task<bool> SaveUserDetails(UserSaveModel userSaveModel);
        Task<bool> GetUserDetails(UserSaveModel userSaveModel);
    }
}

