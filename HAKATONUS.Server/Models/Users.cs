using Microsoft.EntityFrameworkCore;

namespace HAKATONUS.Server.Models

{
    public class Users
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
