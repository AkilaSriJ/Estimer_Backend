using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectManagerId { get; set; }
        public int ProjectStatusId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public int? PaymentTerms { get; set; }
        public decimal? FinalBillingAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Client Client { get; set; }
        public User ProjectManager { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public User CreatedByUser { get; set; }
        public User UpdatedByUser { get; set; }
        public ICollection<ProjectTeamMember> TeamMembers { get; set; }
        public ICollection<ProjectMilestone> Milestones { get; set; }
        public ICollection<Estimate> Estimates { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
