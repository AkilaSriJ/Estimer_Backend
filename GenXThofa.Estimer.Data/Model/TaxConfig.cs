using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Model
{
    public class TaxConfig
    {
        public int TaxConfigId { get; set; }
        public string TaxName { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Estimate> Estimates { get; set; }
    }
}
