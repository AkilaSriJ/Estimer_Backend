using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class ProjectMilestone
    {
        public int ProjectMilestoneId { get; set; }
        public int? ProjectId { get; set; }
        public string MilestoneName { get; set; }
        public int? SequenceNumber { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? MilestoneAmount { get; set; }
        public int MilestoneStatusId { get; set; }
        public DateTime? PlannedCompletionDate { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
        public DateTime? PaymentReceivedDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public Project Project { get; set; }
        public MilestoneStatus MilestoneStatus { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
