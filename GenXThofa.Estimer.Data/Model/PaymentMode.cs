using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class PaymentMode
    {
        public int PaymentModeId { get; set; }
        public string ModeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<PaymentReceipt> PaymentReceipts { get; set; }
    }
}
