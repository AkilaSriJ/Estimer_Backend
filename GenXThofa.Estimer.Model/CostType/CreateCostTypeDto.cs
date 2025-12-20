using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.CostType
{
    public class CreateCostTypeDto
    {
        public string CostTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool RequiresQuantity { get; set; } = true;
        public bool RequiresRate { get; set; } = true;
        public bool RequiresMonths { get; set; } = false;
        public int? DisplayOrder { get; set; }
    }
}
