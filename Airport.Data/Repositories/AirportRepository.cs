using Airport.Models.ModelsForDB;
using AirportSimolator.DataContext;
using AirportSimolator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Airport.Data.Repositories
{
    public class AirportRepository : IAirportRepository

    {
        private readonly IServiceProvider _serviceProvider;
        
        public AirportRepository(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public void AddPlane(Plane plane)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();
                //if (plane.PlaneId == 0)
                //    plane.PlaneId = 1;

                if (plane != null)
                {
                    context.Planes.Add(plane);
                    context.SaveChanges();
                }
            }
        }

        public async Task<Plane> GetPlaneById(int id)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();

                if (id != 0)
                {
                    return await context.Planes.Where(p => p.PlaneId == id).FirstOrDefaultAsync();
                }
                else return null;
            }
        }

        public async Task<List<Plane>> GetPlanes()
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();

                return await context.Planes.ToListAsync();
            }
        }

        public void RemovePlane(Plane plane)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();
                if (plane != null)
                {
                    context.Planes.Remove(plane);
                    context.SaveChanges();
                }
            }
        }

        public async Task<List<StationForDB>> GetStations()
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();
                return await context.Stations.ToListAsync();
            }
        }

        public void AddStation(StationForDB stationForDB)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();
                if (stationForDB != null)
                {
                    context.Stations.Add(stationForDB);
                    context.SaveChanges();
                }
            }
        }

        public async Task UpdateStation(int stationId, int? planeId, string planeName, bool isEmpty)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();

                var station = context.Stations.Find(stationId);
                station.CurrectPlaneId = planeId;
                station.IsEmpty = isEmpty;
                station.CurrectPlaneName = planeName;
                //_airportContext.Stations.Update(station);
                await context.SaveChangesAsync();
            }
        }

        public void UpdateIsPlaneLanded(int planeId, bool isLanded)
        {
            using (var scoped = _serviceProvider.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AirportContext>();
                var plane = context.Planes.Find(planeId);
                plane.IsLanded = isLanded;
                context.Planes.Update(plane);
                context.SaveChanges();
            }
        }
    }
}
