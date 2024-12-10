using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PhoneAttribute MobileNumber { get; set; }
        public Role Role { get; set; }
    }
}
