using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class EstimateDirectCost
    {
        public int EstimateDirectCostId { get; set; }
        public int? EstimationId { get; set; }
        public int CostTypeId { get; set; }
        public string CostName { get; set; }
        public decimal? QuantityOrHours { get; set; }
        public decimal? RateOrCost { get; set; }
        public int? MonthsUsed { get; set; }
        public decimal? TotalCost { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Estimate Estimation { get; set; }
        public CostType CostType { get; set; }
    }
}
