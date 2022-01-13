using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Business.Services
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
