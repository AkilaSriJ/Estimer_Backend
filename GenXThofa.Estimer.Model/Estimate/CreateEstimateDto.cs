using GenXThofa.Technologies.Estimer.Model.EstimateAdditionalCost;
using GenXThofa.Technologies.Estimer.Model.EstimateDirectCost;
using GenXThofa.Technologies.Estimer.Model.EstimateLaborCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Estimate
{
    public class CreateEstimateDto
    {
        public int ProjectId { get; set; }
        public decimal VersionNumber { get; set; }
        public int StatusId { get; set; }
        public decimal ProfitPercentage { get; set; }
        public int? TaxId { get; set; }
        public string ClientRemarks { get; set; }
        public List<CreateEstimateLaborCostDto> LaborCosts { get; set; }
        public List<CreateEstimateDirectCostDto> DirectCosts { get; set; }
        public List<CreateEstimateIndirectCostDto> IndirectCosts { get; set; }
        public List<CreateEstimateAdditionalCostDto> AdditionalCosts { get; set; }
    }
}
