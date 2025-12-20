using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Project
{
    public class UpdateProjectDto
    {   
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public int? ClientId { get; set; }
        public int? ProjectManagerId { get; set; }
        public int ProjectStatusId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
    }
}
