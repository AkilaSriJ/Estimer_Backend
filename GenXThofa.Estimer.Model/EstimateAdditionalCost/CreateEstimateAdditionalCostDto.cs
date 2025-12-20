using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.EstimateAdditionalCost
{
    public class CreateEstimateAdditionalCostDto
    {
        public int? EstimationId { get; set; }
        public int CostTypeId { get; set; }
        public string CostName { get; set; }
        public decimal? CostAmount { get; set; }
        public string Notes { get; set; }
    }
}
