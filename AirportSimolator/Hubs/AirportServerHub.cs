using Airport.Business.Services;
using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimolator.Hubs
{
    public class AirportServerHub : Hub
    {
        private readonly IPlanesService _planesService;
        
        private readonly IAirportLogic _airportLogic;
        public AirportServerHub(IPlanesService planesService , IAirportLogic airportLogic ,  IHubContext<AirportClientHub> clientHub)
        {
            this._airportLogic = airportLogic;
            this._planesService = planesService;
            
        }

        public async Task LandPlane(string planeName )
        {
            //creating plane and adding to DB
            var plane = new Plane { PlaneName = planeName};
            _planesService.AddPlane(plane);
            var planes = await  _planesService.GetPlanes();
            var count = planes.Count;
            //starting the land process
             _airportLogic.LandPlane(plane);

            var msg = $"plane {plane.PlaneName} is on the way to land";
            await Clients.All.SendAsync("LandPlane", msg , count);
        }

        public async Task DepartPlane(string planeName)
        {
            var planes = await _planesService.GetPlanes();
            var departPlane = planes.Find(plane => plane.PlaneName == planeName && plane.IsLanded == true);//finding the correct plane
            var msg = "";
            if (departPlane != null)
            {
                 _airportLogic.DepartPlane(departPlane);
                 msg = $"plane {departPlane.PlaneName} is going to depart";
            }
            else
            {
                 msg = $"there is no such a plane : {planeName}";
            }
            await Clients.All.SendAsync("DepartPlane", msg);
        }

        public async Task GetPlanes()
        {
            var planes = _planesService.GetPlanes();
            await Clients.All.SendAsync("GetPlanes", planes);
        }
    }
}
