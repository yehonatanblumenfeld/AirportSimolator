using Airport.Models.ModelsForDB;
using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimolator.Interfaces
{
    public interface IAirportClientHub
    { 
        Task SendUpdatedStations(List<StationForDB> stations);

        Task sendPlaneUpdate(string msg);

        Task LandPlane(Plane plane);

        Task AutoLand();

        Task AutoDepart();

        Task DepartPlane(Plane plane);

        Task GetStations(List<StationForDB> stations);
    }
}
