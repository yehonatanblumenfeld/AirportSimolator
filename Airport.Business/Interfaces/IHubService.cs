using Airport.Models.ModelsForDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Business.Interfaces
{
    public interface IHubService
    {
        Task SendUpdatedStations(List<StationForDB> stations);

        Task sendPlaneUpdate(string planeName, int stationId);
     
    }
}
