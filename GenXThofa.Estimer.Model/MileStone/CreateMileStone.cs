using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.MileStone
{
    public class CreateMileStone
    {
        public int ProjectId { get; set; }
        public string MilestoneName { get; set; }
        public string Description { get; set; }
        public decimal? PaymentPercentage { get; set; }
        public decimal? MilestoneAmount { get; set; }
        public int MilestoneStatusId { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
