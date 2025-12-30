using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ProjectTeamMember
{
    public class CreateProjectTeamMemberDto
    {
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public int EstimatedHours { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    }
}
