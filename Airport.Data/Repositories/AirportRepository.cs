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

        private readonly AirportContext _airportContext;
        public AirportRepository(AirportContext airportContext) => _airportContext = airportContext;

        public  void AddPlane(Plane plane)
        {

            //if (plane.PlaneId == 0)
            //    plane.PlaneId = 1;

            if (plane != null)
            {
                _airportContext.Planes.Add(plane);
                _airportContext.SaveChanges();
            }
        }

        public async Task<Plane> GetPlaneById(int id)
        {
            if (id != 0)
            {
                return await _airportContext.Planes.Where(p => p.PlaneId == id).FirstOrDefaultAsync();
            }
            else return null;
        }

        public async Task<List<Plane>> GetPlanes()
        {

            return await _airportContext.Planes.ToListAsync();
        }

        public  void RemovePlane(Plane plane)
        {
            if (plane != null)
            {
                _airportContext.Planes.Remove(plane);
                _airportContext.SaveChanges();
            }
        }

        public async Task<List<StationForDB>> GetStations()
        {
            return await _airportContext.Stations.ToListAsync();

        }

        public  void AddStation(StationForDB stationForDB)
        {
            if (stationForDB != null)
            {
                 _airportContext.Stations.Add(stationForDB);
                 _airportContext.SaveChanges();
            }
        }

        public async Task UpdateStation(int stationId, int? planeId, string planeName ,bool isEmpty)
        {
            var station = _airportContext.Stations.Find(stationId);
            station.CurrectPlaneId = planeId;
            station.IsEmpty = isEmpty;
            station.CurrectPlaneName = planeName;
            //_airportContext.Stations.Update(station);
            await _airportContext.SaveChangesAsync();
        }

        public void UpdateIsPlaneLanded(int planeId, bool isLanded)
        {
            var plane =  _airportContext.Planes.Find(planeId);
            plane.IsLanded = isLanded;
            _airportContext.Planes.Update(plane);
            _airportContext.SaveChanges();
        }
    }
}
