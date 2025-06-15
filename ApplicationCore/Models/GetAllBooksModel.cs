using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class GetAllBooksModel
    {
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public int RemainingQTY { get; set; }
        public bool IsBook { get; set; }
    }
}
