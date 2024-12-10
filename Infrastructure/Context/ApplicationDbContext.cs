using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> RoleTable => Set<Role>();
        public DbSet<User> UserTable => Set<User>();
        public DbSet<ScreenSeat> SeatTable => Set<ScreenSeat>();
        public DbSet<TheatreScreen> ScreenTable => Set<TheatreScreen>();
        public DbSet<Theatre> TheatreTable => Set<Theatre>();
        public DbSet<Movie> MovieTable => Set<Movie>();
    }
}
