namespace LibraryManagementAPI.Models
{
    public class UserDetailOutputModel
    {
        public string UserNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public int RoleID { get; set; }
    }
}
