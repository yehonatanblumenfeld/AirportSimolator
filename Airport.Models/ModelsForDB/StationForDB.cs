using System.ComponentModel.DataAnnotations;

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
