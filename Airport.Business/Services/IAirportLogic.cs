using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Business.Services
{
    public interface IAirportLogic
    {
        void LandPlane(Plane plane);

        void DepartPlane(Plane plane);

        Task EnterToStation(Station currectStation , Plane plane, Station prevStation);

    }
}
