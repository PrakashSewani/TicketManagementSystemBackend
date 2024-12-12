using Application.Models.UserAuthentication;
using Domain;

namespace Application.Repositories
{
    public interface IUserAuthRepository
    {
        public Task<User> RegisterUserAsync(User User);
        public Task<bool> DeleteUserAsync(User User);
        public Task<User> UpdateUserAsync(User User);
        public Task<LoggedInUser> LoginUserAsync(string EmailId, string Password);
        public Task<User> GetUserByIdAsync(Guid Id);
        public Task<bool> ForgotPasswordAsync(User User);
        public Task<string> GenerateJwtTokenAsync(User User);
        public Task<bool> ValidateUserRoleClaim(Guid UserId);
        public Task<List<User>> GetAllUsersAsync();
    }
}
