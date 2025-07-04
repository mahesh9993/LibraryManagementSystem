﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LibraryManagementDekstop.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public int BookCategoryID { get; set; }
        public string BookCategoryName { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int NoofCopies { get; set; }
        public int CreatedBy { get; set; }
        public string BookNumber { get; set; }
        public string Status { get; set; }

    }
}
