using Airport.Business.Interfaces;
using Airport.Models.ModelsForDB;
using AirportSimolator.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportSimolator.Hubs
{
    public class HubService : IHubService

    {
        private readonly IHubContext<AirportClientHub, IAirportClientHub> _hubContext;

        public HubService(IHubContext<AirportClientHub, IAirportClientHub> hubContext, IStationService stationService) => _hubContext = hubContext;

        public async Task sendPlaneUpdate(string planeName, int stationId)
        {
            var msg = $"plane - {planeName} has moved to station {stationId}";
            await _hubContext.Clients.All.sendPlaneUpdate(msg);
        }

        public Task SendUpdatedStations(List<StationForDB> updatedStations) => _hubContext.Clients.All.SendUpdatedStations(updatedStations);
    }
}
