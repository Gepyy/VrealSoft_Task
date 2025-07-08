using Microsoft.EntityFrameworkCore;

namespace VrealSoft_Task.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<User> User { get; set; }
    }
} 