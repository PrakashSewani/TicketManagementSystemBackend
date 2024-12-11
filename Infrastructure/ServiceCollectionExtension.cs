using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddDbContext<ApplicationDbContext>(options => options
               .UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
               .AddTransient<IUserAuthRepository, UserAuthRepository>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            return services;
        }
    }
}
