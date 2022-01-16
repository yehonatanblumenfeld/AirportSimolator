using Airport.Business.Interfaces;
using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AirportSimolator.Hubs
{
    public class AirportServerHub : Hub
    {
        private readonly IPlanesService _planesService;
        private readonly IAirportLogic _airportLogic;

        public AirportServerHub(IPlanesService planesService, IAirportLogic airportLogic, IHubContext<AirportClientHub> clientHub)
        {
            this._airportLogic = airportLogic;
            this._planesService = planesService;

        }

        public async Task LandPlane(string planeName)

        {
            var planes = await _planesService.GetPlanes();

            //checks if there is another plane with the same name
            var planeWithSameName = planes.Find(p => p.PlaneName.ToLower() == planeName.ToLower());

            //if there is a plane with the same name return
            if (planeWithSameName != null) return;

            //creating plane and adding to DB
            var plane = new Plane { PlaneName = planeName };
            _planesService.AddPlane(plane);
            var count = planes.Count + 1;

            //starting the land process
            _airportLogic.LandPlane(plane);

            var msg = $"plane {plane.PlaneName} is on the way to land";
            await Clients.All.SendAsync("LandPlane", msg, count);
        }

        public async Task DepartPlane(string planeName)
        {

            var planes = await _planesService.GetPlanes();
            var plane = planes.Find(plane => plane.PlaneName.ToLower() == planeName.ToLower()); ;//finding the correct plane
            var msg = "";
            if (plane != null)
            {
                if (!plane.IsLanded)
                {
                    msg = $"plane {plane.PlaneName} is still in the landing process";
                }
                else
                {
                    await _airportLogic.DepartPlane(plane);
                    msg = $"plane {plane.PlaneName} is on the way to depart";
                }
            }
            else
            {
                msg = $"there is no such a plane : {planeName}";
            }
            await Clients.All.SendAsync("DepartPlane", msg);
        }

        public async Task DepartPlanesAuto()
        {
            var planes = await _planesService.GetPlanes();
            var msg = "";

            foreach (var plane in planes)
            {
                if (plane.IsLanded)
                {
                    await _airportLogic.DepartPlane(plane);
                    msg = $"plane {plane.PlaneName} is on the way to depart";
                    await Clients.All.SendAsync("DepartPlane", msg);
                }
            }

        }
    }
}
