using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimolator.DataContext
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options) : base(options) { }
     

        public DbSet<Plane> Planes { get; set; }
        public DbSet<StationForDB> Stations  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<StationForDB>().HasData(
                
                 new { StationId = 1, IsEmpty = true  , TimeInStation = new TimeSpan(0,0,1)},
                 new { StationId = 2, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 1)},
                 new { StationId = 3, IsEmpty = true , TimeInStation = new TimeSpan(0, 0, 1)},
                 new { StationId = 4, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 2)},
                 new { StationId = 5, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 1)},
                 new { StationId = 6, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 1)},
                 new { StationId = 7, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 1)},
                 new { StationId = 8, IsEmpty = true, TimeInStation = new TimeSpan(0, 0, 1)}
            );
           // modelBuilder.Entity<Plane>().HasData(

           //     new { PlaneId = 1 , PlaneName = "14252" , IsLanded = true },
           //     new { PlaneId = 2, PlaneName = "123", IsLanded = true }


           //);
            base.OnModelCreating(modelBuilder);
        }
    }     
}
