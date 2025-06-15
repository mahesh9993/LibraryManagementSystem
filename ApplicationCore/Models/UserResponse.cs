namespace ApplicationCore.Models
{
    public class UserResponse
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
