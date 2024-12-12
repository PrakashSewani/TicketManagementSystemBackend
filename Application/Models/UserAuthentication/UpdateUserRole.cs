using Domain;

namespace Application.Models.UserAuthentication
{
    public class UpdateUserRole
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
