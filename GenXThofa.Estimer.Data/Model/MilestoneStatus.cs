using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class MilestoneStatus
    {
        public int MilestoneStatusId { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public int? DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<ProjectMilestone> Milestones { get; set; }
    }
}
