using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class EstimateIndirectCost
    {
        public int EstimateIndirectCostId { get; set; }
        public int? EstimationId { get; set; }
        public int CostTypeId { get; set; }
        public string CostName { get; set; }
        public decimal? CostAmount { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Estimate Estimation { get; set; }
        public CostType CostType { get; set; }
    }
}
