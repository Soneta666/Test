using Core.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Infrastructure.Data
{
    internal class TestDbContext : DbContext
    {
        public TestDbContext() : base() { }
        public TestDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConferenceHallConfigurations());
            modelBuilder.ApplyConfiguration(new HallReservationConfigurations());
            modelBuilder.ApplyConfiguration(new ServiceConfigurations());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<ConferenceHall> ConferenceHalls { get; set; }
        public DbSet<HallReservation> HallReservations { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
