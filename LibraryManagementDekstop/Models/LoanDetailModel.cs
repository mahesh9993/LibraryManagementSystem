using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Models
{
    public class LoanDetailModel
    {
        public int UserID { get; set; }
        public int LoanCount { get; set; }
        public int ReservationCount { get; set; }
        public bool HasOverdue { get; set; }
    }
}
