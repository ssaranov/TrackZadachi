using TrackZadach.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackZadach.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Mission> Missions { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
