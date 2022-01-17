using AirportSimolator.Models;
using System.Threading.Tasks;

namespace Airport.Business.Interfaces
{
    public interface IAirportLogic
    {
        Task LandPlane(Plane plane);

        Task DepartPlane(Plane plane);

        Task EnterToStation(Station currectStation , Plane plane, Station prevStation);

    }
}
