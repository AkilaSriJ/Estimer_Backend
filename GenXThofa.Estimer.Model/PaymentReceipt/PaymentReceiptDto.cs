using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.PaymentReceipt
{
    public class PaymentReceiptDto
    {
        public int ReceiptId { get; set; }
        public int? InvoiceId { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? AmountReceived { get; set; }
        public int PaymentModeId { get; set; }
        public string TransactionReference { get; set; }
        public string Narration { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
