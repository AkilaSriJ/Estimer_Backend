using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class EstimationStatus
    {
        public int EstimationStatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Estimate> Estimates { get; set; }
    }
}
