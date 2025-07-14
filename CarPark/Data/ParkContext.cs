using CarPark.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPark.Data
{
    public class ParkContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<MaintenanceRecord> Records { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=Park145.db");
    }
}
