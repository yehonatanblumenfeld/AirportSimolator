using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AirportSimolator.Models
{
    public class Station
    {
        public int StationId { get; set; }
        public Plane CurrectPlane { get; set; }
        public StationStateEnum StationState { get; set; }
        public bool IsEmpty { get;  set; }
        public TimeSpan TimeInStation { get; set; }
        public SemaphoreSlim Semaphore { get; set; }

        public enum StationStateEnum
        {
            Landing = 1,           
            TakeOff,
            Parking,
            LandingAndTakeOff
        }
        public Station(int stationId, bool isEmpty, StationStateEnum stationState , TimeSpan timeInStation)
        {
            StationId = stationId;
            StationState = stationState;
            IsEmpty = isEmpty;
            TimeInStation = timeInStation;
            Semaphore = new SemaphoreSlim(1);

        }
        public async Task EnterStation(Plane plane)
        {
            await this.Semaphore.WaitAsync();
            CurrectPlane = plane;
            IsEmpty = false;
            await Task.Delay(TimeInStation);
            CurrectPlane = null;
            IsEmpty = true;
            this.Semaphore.Release();
        }    
    }
}
