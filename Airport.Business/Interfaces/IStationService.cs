using Airport.Models.ModelsForDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Business.Interfaces
{
    public interface IStationService
    {
        public Task<List<StationForDB>> GetStations();

        void AddStation(StationForDB stationForDB);

        Task UpdateStation(int stationId, int? planeId, string planeName , bool isEmpty);
        

    }
}
