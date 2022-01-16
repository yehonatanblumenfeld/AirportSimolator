using AirportSimolator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Business.Interfaces
{
     public interface IPlanesService
    {
        Task<List<Plane>> GetPlanes();

        Task<Plane> GetPlaneById(int id);
        
        void AddPlane(Plane plane);

        void RemovePlane(Plane plane);

        void UpdateIsPlaneLanded(int planeId, bool isLanded);

    }
}
