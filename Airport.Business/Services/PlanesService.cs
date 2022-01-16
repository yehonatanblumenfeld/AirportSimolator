using Airport.Business.Interfaces;
using Airport.Data.Repositories;
using AirportSimolator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Business.Services
{
    public class PlanesService : IPlanesService
    {
        private readonly IAirportRepository _airportRepository;

        public PlanesService(IAirportRepository _airportRepository) => this._airportRepository = _airportRepository;


        public async Task<List<Plane>> GetPlanes()
        {
            return await _airportRepository.GetPlanes();
        }

        public async Task<Plane> GetPlaneById(int id)
        {
            return await _airportRepository.GetPlaneById(id);
        }

        public void AddPlane(Plane plane)
        {
            _airportRepository.AddPlane(plane);
        }

        public void RemovePlane(Plane plane)
        {
            _airportRepository.RemovePlane(plane);
        }

        public void UpdateIsPlaneLanded(int planeId, bool isLanded)
        {
            _airportRepository.UpdateIsPlaneLanded(planeId, isLanded);
        }      
    }
}


