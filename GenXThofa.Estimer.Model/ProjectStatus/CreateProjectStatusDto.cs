using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.ProjectStatus
{
    public class CreateProjectStatusDto
    {
        public string StatusName { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        public string StatusColor { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
