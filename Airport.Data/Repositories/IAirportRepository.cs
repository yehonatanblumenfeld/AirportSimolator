using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Data.Repositories
{
    public interface IAirportRepository
    {
        Task<List<Plane>> GetPlanes();

        Task<Plane> GetPlaneById(int id);

        void AddPlane(Plane plane);

        void RemovePlane(Plane plane);

        void UpdateIsPlaneLanded(int planeId, bool isLanded);

        Task<List<StationForDB>> GetStations();

        void AddStation(StationForDB stationForDB);

        Task UpdateStation(int stationId, int? planeId, string planeName , bool isEmpty);

    }
}
