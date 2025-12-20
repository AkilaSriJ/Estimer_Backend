using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.EstimationStatus
{
    public class EstimationStatusDto
    {
        public int EstimationStatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
