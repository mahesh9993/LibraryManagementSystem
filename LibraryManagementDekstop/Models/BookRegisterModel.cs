using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementDekstop.Models
{
    public class BookRegisterModel
    {
        public int ISBN { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int Category { get; set; }
        public int BookCount { get; set; }
    }
}
