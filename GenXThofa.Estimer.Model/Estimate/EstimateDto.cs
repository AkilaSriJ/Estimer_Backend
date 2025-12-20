using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Estimate
{
    public class EstimateDto
    {
        public int EstimationId { get; set; }
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
    }
}
