using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.EstimateDirectCost
{
    public class CreateEstimateDirectCostDto
    {
        public int? EstimationId { get; set; }
        public int CostTypeId { get; set; }
        public string CostName { get; set; }
        public decimal? QuantityOrHours { get; set; }
        public decimal? RateOrCost { get; set; }
        public int? MonthsUsed { get; set; }
        public string Notes { get; set; }
    }
}
