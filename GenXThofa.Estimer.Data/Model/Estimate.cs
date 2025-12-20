using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class Estimate
    {
        public int EstimateId { get; set; }
        public int? ProjectId { get; set; }
        public decimal? VersionNumber { get; set; }
        public int EstimationStatusId { get; set; }
        public decimal? TotalDirectCost { get; set; }
        public decimal? TotalIndirectCost { get; set; }
        public decimal? TotalAdditionalCost { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? ProfitPercentage { get; set; }
        public decimal? ProfitAmount { get; set; }
        public int? TaxId { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? FinalAmount { get; set; }
        public string ClientRemarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Project Project { get; set; }
        public EstimationStatus EstimationStatus { get; set; }
        public TaxConfig Tax { get; set; }
        public ICollection<EstimateLaborCost> LaborCosts { get; set; }
        public ICollection<EstimateDirectCost> DirectCosts { get; set; }
        public ICollection<EstimateIndirectCost> IndirectCosts { get; set; }
        public ICollection<EstimateAdditionalCost> AdditionalCosts { get; set; }
    }
}
