using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class ClientAuditLog
    {
        public int ClientAuditLogId { get; set; }
        public string ModuleName { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        public int? ActionTypeId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; } 
        public string Description { get; set; }
        public int? PerformedBy { get; set; }
        public DateTime PerformedAt { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public EntityType EntityType { get; set; }
        public ActionType ActionType { get; set; }
        public User PerformedByUser { get; set; }
    }
}
