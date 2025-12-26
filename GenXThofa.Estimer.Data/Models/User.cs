using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Role Role { get; set; }
        public ICollection<Client> CreatedClients { get; set; }
        public ICollection<Client> UpdatedClients { get; set; }
        public ICollection<Project> ManagedProjects { get; set; }
        public ICollection<Project> CreatedProjects { get; set; }
        public ICollection<Project> UpdatedProjects { get; set; }
        public ICollection<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public ICollection<EstimateLaborCost> EstimateLaborCosts { get; set; }
    }
}
