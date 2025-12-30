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
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName {  get; set; }
        public string Designation { get; set; }
        public decimal? HourlyRate { get; set; }
        public int EstimatedHours {  get; set; }
        public decimal TotalCost {  get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Project Project { get; set; }
        public User Employee { get; set; }
        public Role Roles { get; set; }
    }
}
