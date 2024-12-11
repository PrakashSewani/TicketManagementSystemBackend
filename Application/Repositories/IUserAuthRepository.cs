using Domain;

namespace Application.Repositories
{
    public interface IUserAuthRepository
    {
        public Task<bool> RegisterUserAsync(User User);
        public Task<bool> DeleteUserAsync(User User);
        public Task<User> UpdateUserAsync(User User);
        public Task<User> LoginUserAsync(string EmailId, string Password);
        public Task<User> GetUserByIdAsync(User User);
        public Task<bool> ForgotPasswordAsync(User User);
        public Task<string> GenerateJwtTokenAsync(User User);
        public Task<bool> ValidateUserRoleClaim(string RoleClaim, User User);
    }
}
