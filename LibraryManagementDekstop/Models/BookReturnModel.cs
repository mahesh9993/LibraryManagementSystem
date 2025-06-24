public class BookReturnModel
{
    public int UserID { get; set; }
    public int BookCopyID { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int CreatedBy { get; set; }
}

public class ApiResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public List<BookReturnModel> data { get; set; }
}
