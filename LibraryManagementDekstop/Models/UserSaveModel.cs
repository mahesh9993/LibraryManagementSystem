using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Models
{
    public class UserSaveModel
    {
        public string UserNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string UserType { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}