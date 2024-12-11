using Domain;

namespace Application.Models.UserAuthentication
{
    public class LoggedInUser
    {
        public User User { get; set; }
        public string BearerToken { get; set; }
    }
}
