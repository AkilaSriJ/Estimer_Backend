using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class ProjectTeamMember
    {
        public int ProjectTeamMemberId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public int TeamRoleId { get; set; }
        public decimal? AllocationPercentage { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Project Project { get; set; }
        public User Employee { get; set; }
        public TeamRole TeamRole { get; set; }
    }
}
