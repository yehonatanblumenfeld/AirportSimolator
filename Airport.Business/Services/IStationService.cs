using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Business.Services
{
    public interface IStationService
    {
        public Task<List<StationForDB>> GetStations();

        void AddStation(StationForDB stationForDB);

        Task UpdateStation(int stationId, int? planeId, string planeName , bool isEmpty);
        

    }
}
