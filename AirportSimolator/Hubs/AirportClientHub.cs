using Airport.Business.Services;
using Airport.Models.ModelsForDB;
using AirportSimolator.Interfaces;
using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AirportSimolator.Hubs
{
    public class AirportClientHub : Hub<IAirportClientHub>
    {
        private readonly IStationService _stationService;
        private readonly IPlanesService _planeService;
        private readonly IAirportLogic _airportLogic;

       
        public AirportClientHub(IStationService stationService, IPlanesService planesService, IAirportLogic airportLogic)
        {
            _stationService = stationService;
            _planeService = planesService;
            _airportLogic = airportLogic;
        }   
   
        //public async Task GetStations()
        //{           
        //    var list = await _stationService.GetStations();    
            
        //}
        //public void LandPlanes()
        //{
        //    var planes = _planeService.GetPlanes();

        //    if (planes.Count == 0) return;
        //    foreach (var airplane in planes)
        //    {
        //        _airportLogic.LandPlane(airplane);
        //    }
        //}

        //public void LandPlane()
        //{
        //    var count = _planeService.GetPlanes().Count;           
        //    var plane = _planeService.GetPlaneById(count);//gets the last plane in the list

        //    if (plane is null) return;
        //    _airportLogic.LandPlane(plane);
        //}

        //public void DepartPlanes()
        //{
        //    //TODO
        //    //error occured when trying to get the planes 
        //    //plane does not removed from the station after taking off 

        //    var landedplanes = _planeService.GetPlanes().FindAll(plane => plane.isLanded is true); 
        //    if (landedplanes.Count == 0) return;
        //    foreach (var airplane in landedplanes)
        //    {
        //        _airportLogic.DepartPlane(airplane);
        //    }
        //}
       
    }
}
