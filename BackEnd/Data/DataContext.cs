using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<VM> VMs { get; set; }
        public DbSet<VHD> VHDs { get; set; }
        public DbSet<CDDVD> CDVDs { get; set; }
        public DbSet<SC> SCs { get; set; }
        public DbSet<Network> Networks { get; set; }
    }
}
