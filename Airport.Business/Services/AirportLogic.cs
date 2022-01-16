using Airport.Business.Interfaces;
using AirportSimolator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Airport.Business.Services
{
    public class AirportLogic : IAirportLogic
    {
        public List<Station> Stations { get; }

        List<Station> _landStations;
        List<Station> _departStations;
        private readonly IStationService _stationService;
        private readonly IPlanesService _planesService;
        private readonly IHubService _hubService;

        public AirportLogic(IStationService stationService, IHubService hubService, IPlanesService planesService)
        {
            _hubService = hubService;
            _stationService = stationService;
            _planesService = planesService;
            Stations = AirportStations.Instance.stations;
            _landStations = AirportStations.Instance.landPath;
            _departStations = AirportStations.Instance.depaturePath;
        }

        public async void LandPlane(Plane plane)
        {
            for (int i = 0; i < _landStations.Count - 1; i++)
            {
                if (_landStations[i].StationState != Station.StationStateEnum.Parking)//checks wither its parking station or not
                {
                    if (i > 0)
                    {

                        await EnterToStation(_landStations[i], plane, _landStations[i - 1]);
                    }
                    else
                    {
                        await EnterToStation(_landStations[i], plane, null);
                    }
                }
                else
                {
                    while (true)
                    {
                        if (_landStations[i].IsEmpty)
                        {

                            await EnterToStation(_landStations[i], plane, _landStations[i - 1]);
                            return;
                        }
                        else if (_landStations[i + 1].IsEmpty)
                        {

                            await EnterToStation(_landStations[i + 1], plane, _landStations[i - 1]);
                            return;
                        }
                    }
                }
            }
        }

        public async Task DepartPlane(Plane plane)
        {
            var firstStation = 0;
            int stationIndex = 0;
            for (int i = 0; i < 3; i++)
            {
                if (stationIndex > 0)
                {
                    if(stationIndex == 2)
                        await EnterToStation(_departStations[stationIndex], plane, _departStations[firstStation]);
                    else
                        await EnterToStation(_departStations[stationIndex], plane, _departStations[stationIndex - 1]);
                    stationIndex++;
                }
                else
                {
                    while (true)
                    {
                        if (_departStations[stationIndex].IsEmpty)
                        {
                            await EnterToStation(_departStations[stationIndex], plane, null);
                            stationIndex = 2; // plane entered to station 6 , so the index most continue from 7
                            firstStation = 0;
                            break;
                        }
                        else if (_departStations[stationIndex + 1].IsEmpty)
                        {

                            await EnterToStation(_departStations[stationIndex + 1], plane, null);
                            stationIndex = 2; // plane entered to station 7 , so the index most continue from 7
                            firstStation = 1;
                            break;
                        }
                    }
                }
            }
        }

        public async Task EnterToStation(Station currectStation, Plane plane, Station prevStation)
        {
            await currectStation.Semaphore.WaitAsync(); //locking the thread cuz we dont want another plane in the station 
            currectStation.CurrectPlane = plane;

            if (prevStation != null)
            {
                await _stationService.UpdateStation(prevStation.StationId, null, null, true); //removing the plane from the previous station 
                prevStation.IsEmpty = true;
                prevStation.Semaphore.Release();
            }

            await _stationService.UpdateStation(currectStation.StationId, plane.PlaneId, plane.PlaneName, false); // updating the station in the DB
            await _hubService.sendPlaneUpdate(plane.PlaneName, currectStation.StationId);
            await _hubService.SendUpdatedStations(await _stationService.GetStations());
            currectStation.IsEmpty = false;

            await Task.Delay(currectStation.TimeInStation);

            if (currectStation.StationState == Station.StationStateEnum.Parking && plane.IsLanded == false)
            {
                await _stationService.UpdateStation(currectStation.StationId, null, null, true); //removing the plane from the previous station 
                _planesService.UpdateIsPlaneLanded(plane.PlaneId, true);
                await _hubService.SendUpdatedStations(await _stationService.GetStations());
                currectStation.IsEmpty = true;
                currectStation.Semaphore.Release();
            }
            else if (plane.IsLanded)
            {
                if (currectStation.StationState == Station.StationStateEnum.LandingAndTakeOff)
                {
                    await _stationService.UpdateStation(currectStation.StationId, null, null, true); //removing the plane from the previous station 
                    _planesService.RemovePlane(plane); // plane removed from planes list
                     await _hubService.SendUpdatedStations(await _stationService.GetStations());
                    currectStation.IsEmpty = true;
                    currectStation.Semaphore.Release();
                }
            }
        }
    }
}
