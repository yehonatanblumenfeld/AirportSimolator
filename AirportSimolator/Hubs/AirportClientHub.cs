using Airport.Business.Interfaces;
using AirportSimolator.Interfaces;
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

        public async Task GetStations()
        {
            var list = await _stationService.GetStations();
            await Clients.All.GetStations(list);
        }
    }
}
