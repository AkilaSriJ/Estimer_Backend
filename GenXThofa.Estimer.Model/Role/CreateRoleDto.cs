using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Model.Role
{
    public class CreateRoleDto
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Permissions { get; set; } 
    }
}
