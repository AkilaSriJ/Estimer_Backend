using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class CostType
    {
        public int CostTypeId { get; set; }
        public string CostTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool RequiresQuantity { get; set; } = true;
        public bool RequiresRate { get; set; } = true;
        public bool RequiresMonths { get; set; } = false;
        public int? DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<EstimateDirectCost> DirectCosts { get; set; }
        public ICollection<EstimateIndirectCost> IndirectCosts { get; set; }
        public ICollection<EstimateAdditionalCost> AdditionalCosts { get; set; }
    }
}
