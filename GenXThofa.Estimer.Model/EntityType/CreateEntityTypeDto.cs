using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.EntityType
{
    public class CreateEntityTypeDto
    {
        public string EntityName { get; set; }
        public string TableName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
