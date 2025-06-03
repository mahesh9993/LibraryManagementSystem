using LibraryManagementDekstop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Interfaces
{
    internal interface IUserService
    {
        Task<bool> SaveUserDetails(UserSaveModel userSaveModel);
    }
}
