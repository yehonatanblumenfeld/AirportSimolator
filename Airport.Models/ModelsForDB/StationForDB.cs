using AirportSimolator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Models.ModelsForDB
{
    public class StationForDB
    {
        [Key]
        public int StationId { get; set; }
        public int? CurrectPlaneId { get; set; }
        public string CurrectPlaneName { get; set; }
        public bool IsEmpty { get; set; }
        public string State { get; set; }
    }
}
