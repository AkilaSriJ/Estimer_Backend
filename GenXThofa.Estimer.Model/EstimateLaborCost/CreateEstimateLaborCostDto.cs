using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.EstimateLaborCost
{
    public class CreateEstimateLaborCostDto
    {
        public int? EstimationId { get; set; }
        public int? EmployeeId { get; set; }
        public decimal? EstimatedHours { get; set; }
        public decimal? HourlyRate { get; set; }
        public decimal? TotalCost { get; set; }
        public string Notes { get; set; }
    }
}
