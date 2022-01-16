using Airport.Models.ModelsForDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSimolator.Interfaces
{
    public interface IAirportClientHub
    {
        Task SendUpdatedStations(List<StationForDB> stations);

        Task sendPlaneUpdate(string msg);

        Task GetStations(List<StationForDB> stations);
    }
}
