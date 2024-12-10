using Application.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserAuthRepository : IUserAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserAuthRepository(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Task<bool> DeleteUserAsync(User User)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(User User)
        {
            throw new NotImplementedException();
        }

        public Task<string> GenerateJwtTokenAsync(User User)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.Name),
                new Claim(JwtRegisteredClaimNames.Email,User.Email),
                new Claim("Role",User.Role.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var Token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: Credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(Token);

            return Task.FromResult(jwtToken);
        }

        public Task<User> GetUserByIdAsync(User User)
        {
            throw new NotImplementedException();
        }

        public Task<User> LoginUserAsync(User User)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUserAsync(User User)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(User User)
        {
            throw new NotImplementedException();
        }
    }
}
