using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ProjectTeamMember
{
    public class ProjectTeamMemberDto
    {
        public int TeamId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public int TeamRoleId { get; set; }
        public decimal? AllocationPercentage { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
