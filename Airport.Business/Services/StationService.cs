using Airport.Data.Repositories;
using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Business.Services
{
    public class StationService : IStationService
    {
        private readonly IAirportRepository _airportRepository;

        public StationService(IAirportRepository airportRepository) => this._airportRepository = airportRepository;

        public async Task<List<StationForDB>> GetStations()
        {
            return await _airportRepository.GetStations();
        }

        public void AddStation(StationForDB stationForDB)
        {
            _airportRepository.AddStation(stationForDB);
        }

        public async Task UpdateStation(int stationId, int? planeId, string planeName, bool isEmpty)
        {
           await _airportRepository.UpdateStation(stationId, planeId, planeName, isEmpty);
        }
    }
}
