using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long MobileNumber { get; set; }

        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
