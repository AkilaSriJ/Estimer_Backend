using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Invoice
{
    public class CreateInvoiceDto
    {
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
        public int InvoiceStatusId { get; set; }
        public string Notes { get; set; }
    }
}
