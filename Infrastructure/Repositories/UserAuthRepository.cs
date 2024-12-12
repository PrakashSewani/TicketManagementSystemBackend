using Application.Models.UserAuthentication;
using Application.Repositories;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> DeleteUserAsync(User User)
        {
            try
            {
                _context.UserTable.Remove(User);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ForgotPasswordAsync(User User)
        {
            try
            {
                User UserInDb = await _context.UserTable.AsQueryable()
                    .Where(u => u.Email == User.Email)
                    .FirstOrDefaultAsync();
                if (UserInDb == null) throw new ArgumentException("User does not exists in the Database");
                UserInDb.Password = User.Password;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<string> GenerateJwtTokenAsync(User User)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var Claims = new[]
            {
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

        public async Task<User> GetUserByIdAsync(Guid Id)
        {
            try
            {
                User UserInDb = await _context.UserTable.Include(d => d.Role).AsQueryable()
                    .Where(u => u.Id == Id)
                    .FirstOrDefaultAsync();
                if (UserInDb == null) throw new ArgumentException("User does not exist in the Database");
                return UserInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LoggedInUser> LoginUserAsync(string EmailId, string Password)
        {
            try
            {
                User UserInDb = await _context.UserTable.Include(d => d.Role).AsQueryable()
                    .Where(u => u.Email == EmailId)
                    .FirstOrDefaultAsync();
                if (UserInDb == null) throw new ArgumentException("User doest not exists in Database, try Register User");
                if (UserInDb.Password != Password) throw new ArgumentException("User EmailId/Password do not match. Try again later");
                string UserToken = await GenerateJwtTokenAsync(UserInDb);
                LoggedInUser LoggedInUser = new LoggedInUser()
                {
                    User = UserInDb,
                    BearerToken = UserToken
                };
                return LoggedInUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> RegisterUserAsync(User User)
        {
            try
            {
                User UserInDb = await _context.UserTable.AsQueryable()
                    .Where(u => u.Email == User.Email)
                    .FirstOrDefaultAsync();
                if (UserInDb != null) throw new ArgumentException("User already exists in Database, try Forgot Password");
                _context.UserTable.Add(User);
                await _context.SaveChangesAsync();
                return await _context.UserTable.Include(d => d.Role).AsQueryable()
                    .Where(u => u.Id == User.Id)
                    .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> UpdateUserAsync(User User)
        {
            try
            {
                _context.UserTable.Update(User);
                await _context.SaveChangesAsync();
                return User;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateUserRoleClaim(string RoleClaim, User User)
        {
            User UserInDb = await GetUserByIdAsync(User.Id);
            if (UserInDb.Role.RoleName == RoleClaim) return true;
            throw new UnauthorizedAccessException("User Action forbidded since User has no sudo access.");
        }
    }
}
