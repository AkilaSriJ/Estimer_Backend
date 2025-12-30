using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ProjectTeamMember
{
    public class ProjectTeamMemberDto
    {
        public int ProjectTeamMemberId { get; set; }
        public int? ProjectId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public decimal? HourlyRate { get; set; }
        public int EstimatedHours { get; set; }
        public decimal TotalCost { get; set; }
    }
}
