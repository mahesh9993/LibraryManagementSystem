using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Models
{
    public class BookReturnModel
    {
        public string UserCode { get; set; }
        public string BookCode { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }

    }
}
