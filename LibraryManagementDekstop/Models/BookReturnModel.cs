public class BookReturnModel
{
    public int userID { get; set; }
    public int bookCopyID { get; set; }
    public DateTime loanDate { get; set; }
    public DateTime returnDate { get; set; }
    public int createdBy { get; set; }
}

public class ApiResponse
{
    public int statusCode { get; set; }
    public string message { get; set; }
    public List<BookReturnModel> data { get; set; }
}
