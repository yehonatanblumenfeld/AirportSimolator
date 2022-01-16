using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using Microsoft.EntityFrameworkCore;

namespace AirportSimolator.DataContext
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }


        public DbSet<Plane> Planes { get; set; }
        public DbSet<StationForDB> Stations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StationForDB>().HasData(

                 new { StationId = 1, IsEmpty = true, State = "Landing" },
                 new { StationId = 2, IsEmpty = true, State = "Landing" },
                 new { StationId = 3, IsEmpty = true, State = "Landing" },
                 new { StationId = 4, IsEmpty = true, State = "LandingAndTakeOff" },
                 new { StationId = 5, IsEmpty = true, State = "Landing" },
                 new { StationId = 6, IsEmpty = true, State = "Parking" },
                 new { StationId = 7, IsEmpty = true, State = "Parking" },
                 new { StationId = 8, IsEmpty = true, State = "TakeOff" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
