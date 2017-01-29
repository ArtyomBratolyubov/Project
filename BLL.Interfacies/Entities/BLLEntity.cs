namespace BLL.Interface.Entities
{
    public class BLLUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}


