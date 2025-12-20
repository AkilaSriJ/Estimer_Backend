using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Tax_Config
{
    public class CreateTax_ConfigDto
    {
        public int TaxId { get; set; }
        public string TaxName { get; set; }
        public decimal? TaxRate { get; set; }
    }
}
