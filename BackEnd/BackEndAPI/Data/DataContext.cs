using Microsoft.EntityFrameworkCore;

namespace BackEndAPI.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<VM> VMs { get; set; }
    }
}
