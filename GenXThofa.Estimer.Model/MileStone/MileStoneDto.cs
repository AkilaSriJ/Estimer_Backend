using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.MileStone
{
    public class MileStoneDto
    {
        public int ProjectMilestoneId { get; set; }
        public int ProjectId { get; set; }
        public string MilestoneName { get; set; }
        public string Description { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? MilestoneAmount { get; set; }
        public int MilestoneStatusId { get; set; }
        public string MileStoneStatusName { get; set; }
        public string StatusColor { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
