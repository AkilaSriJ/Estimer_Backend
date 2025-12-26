using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ProjectId { get; set; }
        public int? MilestoneId { get; set; }
        public int? ClientId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal? AdvancePercentage { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TaxPercentage { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? AmountPaid { get; set; }
        public decimal? BalanceDue { get; set; }
        public int InvoiceStatusId { get; set; }
        public string Notes { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public Project Project { get; set; }
        public ProjectMilestone Milestone { get; set; }
        public Client Client { get; set; }
        public PaymentType PaymentType { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public ICollection<PaymentReceipt> PaymentReceipts { get; set; }
    }
}
