using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Invoice
{
    public class InvoiceDto
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
        public string InvoiceStatusName { get; set; }
        public string Notes { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
