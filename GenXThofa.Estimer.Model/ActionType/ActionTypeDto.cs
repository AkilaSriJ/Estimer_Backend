using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ActionType
{
    public class ActionTypeDto
    {
        public int ActionTypeId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
