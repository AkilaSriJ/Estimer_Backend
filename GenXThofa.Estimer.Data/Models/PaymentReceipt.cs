using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class PaymentReceipt
    {
        public int PaymentReceiptId { get; set; }
        public int? InvoiceId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public int PaymentModeId { get; set; }
        public string TransactionReference { get; set; }
        public string Narration { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public Invoice Invoice { get; set; }
        public PaymentMode PaymentMode { get; set; }
    }
}
