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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("3cab6048-0f28-4d60-828a-882cc2baa90d"),
                    RoleName = "TheatreOwner"
                },
                new Role
                {
                    Id = Guid.Parse("6f2a960a-7d3b-4f61-881b-22ee6c319948"),
                    RoleName = "TheatreUser"
                },
                new Role
                {
                    Id = Guid.Parse("459a516b-7e76-4aca-a759-1ae1b100aa2d"),
                    RoleName = "PlatformAdmin"
                }
                );
        }
    }
}
