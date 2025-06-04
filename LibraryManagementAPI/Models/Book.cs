namespace LibraryManagementAPI.Models
{
    public class Book
    {
        public string BookNumber { get; set; }
        public string ISBN { get; set; }
        public int BookCategoryID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int NoofCopies { get; set; }
        public int CreatedBy { get; set; }

    }
}
