using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirportSimolator.Models
{
    public class Plane
    {
        public int PlaneId { get; set; }
        public string PlaneName { get; set; }
        public bool IsLanded { get; set; }// represents if the plane has already landed

    }
}
