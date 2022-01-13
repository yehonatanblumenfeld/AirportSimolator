using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using static AirportSimolator.Models.Station;

namespace Airport.Business.Services
{
    public sealed class AirportStations
    {
        private static AirportStations _instance = null;
        public static AirportStations Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new AirportStations();
                }
                return _instance;
            }
        }
        public List<Station> stations { get; set; }
        public List<Station> landPath { get; set; }
        public List<Station> depaturePath { get; set; }

        public AirportStations()
        {
            // initializing stations
            stations = new List<Station>() {
                new Station(1 , true , StationStateEnum.Landing , new TimeSpan(0,0,1)),
                new Station(2,  true , StationStateEnum.Landing , new TimeSpan(0, 0, 1)),
                new Station(3,  true , StationStateEnum.Landing , new TimeSpan(0, 0, 1)),
                new Station(4,  true , StationStateEnum.LandingAndTakeOff , new TimeSpan(0, 0, 3)),
                new Station(5,  true , StationStateEnum.Landing , new TimeSpan(0, 0, 1)),
                new Station(6,  true , StationStateEnum.Parking , new TimeSpan(0, 0, 2)),
                new Station(7,  true , StationStateEnum.Parking , new TimeSpan(0, 0, 2)),
                new Station(8,  true , StationStateEnum.TakeOff , new TimeSpan(0, 0, 1))
            };
            landPath = new List<Station>();
            depaturePath = new List<Station>();

            landPath.Add(stations[0]); //station 1 landing
            landPath.Add(stations[1]); //station 2 landing
            landPath.Add(stations[2]); //station 3 landing
            landPath.Add(stations[3]); //station 4 LandingAndTakeOff
            landPath.Add(stations[4]); //station 5 landing
            landPath.Add(stations[5]); //station 6 Parking
            landPath.Add(stations[6]); //station 7 Parking

            depaturePath.Add(stations[5]); //station 6 Parking
            depaturePath.Add(stations[6]); //station 7 Parking
            depaturePath.Add(stations[7]); //station 8 TakeOff
            depaturePath.Add(stations[3]); //station 4 LandingAndTakeOff

        }
    }
}
