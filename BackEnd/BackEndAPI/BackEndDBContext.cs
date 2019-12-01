using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndAPI
{
    public class BackEndDBContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<VM> VMs { get; set; }

        public BackEndDBContext(DbContextOptions<BackEndDBContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(35);
            modelBuilder.Entity<User>()
                .Property(u => u.Country)
                .IsRequired()
                .HasMaxLength(40);
            modelBuilder.Entity<User>()
                .Property(u => u.Rights)
                .IsRequired()
                .HasMaxLength(30);

            modelBuilder.Entity<VM>()
                .Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<VM>()
                .Property(v => v.RealID)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<VM>()
                .Property(v => v.Configuration)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<VM>().HasData(
                new
                {
                    VMId = Guid.NewGuid(),
                    RealID = "sag1_s15sag3g9121410",
                    Name="MyFirstVM1",
                    Configuration= "vms/sag1_s15sag3g9121410/config.json",
                    LastSave= "/vms/sag1_s15sag3g9121410/save.json",
                },
                new
                {
                    VMId = Guid.NewGuid(),
                    RealID = "sag1_s15sag3g9121asa1210",
                    Name = "MyFirstVM2",
                    Configuration = "vms/sag1_s15sag3g9121asa1210/config.json",
                    LastSave = "/vms/sag1_s15sag3g9121asa1210/save.json",
                },
                new
                {
                    VMId = Guid.NewGuid(),
                    RealID = "sag1_s15sag3g9121asa1210abcd123",
                    Name = "MyFirstVM3",
                    Configuration = "vms/sag1_s15sag3g9121asa1210abcd123/config.json",
                    LastSave = "/vms/sag1_s15sag3g9121asa1210abcd123/save.json",
                });

            modelBuilder.Entity<User>().HasData(
                new
                {
                    UserId=Guid.NewGuid(),
                    FirstName="AAA",
                    LastName="BBB",
                    Username="ababa124",
                    Country="Romania",
                    Password="blablabla192",
                    Rights="rwx"
                },
                new
                {
                    UserId = Guid.NewGuid(),
                    FirstName = "CCCC",
                    LastName = "DD",
                    Username = "cd1234",
                    Country = "Germany",
                    Password = "cd113",
                    Rights = "rx"
                });
        }
    }
}
