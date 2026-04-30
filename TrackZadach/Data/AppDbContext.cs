using TrackZadach.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackZadach.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

    }
}
