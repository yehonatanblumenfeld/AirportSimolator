using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Business.Interfaces
{
    public interface IHubService
    {
        Task SendUpdatedStations(List<StationForDB> stations);

        Task sendPlaneUpdate(string planeName, int stationId);

        Task LandPlane(Plane plane);

        Task AutoLand();

        Task AutoDepart();
     
        Task DepartPlane(Plane plane);
      
    }
}
