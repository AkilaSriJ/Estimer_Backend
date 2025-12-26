using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class EstimateLaborCost
    {
        public int EstimateLaborCostId { get; set; }
        public int? EstimationId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Estimate Estimation { get; set; }
        public User Employee { get; set; }
    }
}
