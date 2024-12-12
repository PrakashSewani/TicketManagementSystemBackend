using Domain;

namespace Application.Models.UserAuthentication
{
    internal class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public Role Role { get; set; }
    }
}
