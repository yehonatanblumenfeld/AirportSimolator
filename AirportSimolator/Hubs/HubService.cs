using Airport.Business.Interfaces;
using Airport.Business.Services;
using Airport.Models.ModelsForDB;
using AirportSimolator.Interfaces;
using AirportSimolator.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimolator.Hubs
{
    public class HubService : IHubService

    {
        private readonly IHubContext<AirportClientHub, IAirportClientHub> _hubContext;

        public HubService(IHubContext<AirportClientHub, IAirportClientHub> hubContext, IStationService stationService)
        {
            _hubContext = hubContext;
        }
        public Task AutoDepart() => _hubContext.Clients.All.AutoDepart();

        public Task AutoLand() => _hubContext.Clients.All.AutoLand();

        public Task DepartPlane(Plane plane) => _hubContext.Clients.All.DepartPlane(plane);

        public Task LandPlane(Plane plane) => _hubContext.Clients.All.LandPlane(plane);

        public async Task sendPlaneUpdate(string planeName, int stationId)
        {
            var msg = $"plane - {planeName} has moved to station {stationId}";
            await _hubContext.Clients.All.sendPlaneUpdate(msg);
        }

        public Task SendUpdatedStations(List<StationForDB> updatedStations) => _hubContext.Clients.All.SendUpdatedStations(updatedStations);
    }
}
