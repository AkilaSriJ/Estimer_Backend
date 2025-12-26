using GenXThofa.Technologies.Estimer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenXThofa.Technologies.Estimer.Data.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){ }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<MilestoneStatus> MilestoneStatuses { get; set; }
        public DbSet<EstimationStatus> EstimationStatuses { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<TeamRole> TeamRoles { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<EntityType> EntityTypes { get; set; }
        public DbSet<TaxConfig> TaxConfigs { get; set; }

        // Core Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTeamMember> ProjectTeamMembers { get; set; }
        public DbSet<ProjectMilestone> ProjectMilestones { get; set; }

        // Estimation Tables
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<EstimateLaborCost> EstimateLaborCosts { get; set; }
        public DbSet<EstimateDirectCost> EstimateDirectCosts { get; set; }
        public DbSet<EstimateIndirectCost> EstimateIndirectCosts { get; set; }
        public DbSet<EstimateAdditionalCost> EstimateAdditionalCosts { get; set; }

        // Invoice & Payment Tables
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentReceipt> PaymentReceipts { get; set; }

        // Audit Log Tables
        public DbSet<ClientAuditLog> ClientAuditLogs { get; set; }
        public DbSet<ProjectAuditLog> ProjectAuditLogs { get; set; }
        public DbSet<InvoiceAuditLog> InvoiceAuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasOne(c=>c.CreatedByUser)
                .WithMany(u=>u.CreatedClients)
                .HasForeignKey(c=>c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Client>()
                .HasOne(c=>c.UpdatedByUser)
                .WithMany(u=>u.UpdatedClients)
                .HasForeignKey(c=>c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.Client)
                .WithMany(c=>c.Projects)
                .HasForeignKey(p=>p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.ProjectManager)
                .WithMany(pm=>pm.ManagedProjects)
                .HasForeignKey(p=>p.ProjectManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.ProjectStatus)
                .WithMany(ps=>ps.Projects)
                .HasForeignKey(p=>p.ProjectStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.CreatedByUser)
                .WithMany(u=>u.CreatedProjects)
                .HasForeignKey(p=>p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p=>p.UpdatedByUser)
                .WithMany(u=>u.UpdatedProjects)
                .HasForeignKey(p=>p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTeamMember>()
                .HasOne(pm=>pm.Project)
                .WithMany(p=>p.TeamMembers)
                .HasForeignKey(pm=>pm.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTeamMember>() 
                .HasOne(pm=>pm.Employee)
                .WithMany(e=>e.ProjectTeamMembers)
                .HasForeignKey(pm=>pm.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectTeamMember>()
                .HasOne(pm=>pm.TeamRole)
                .WithMany(t=>t.TeamMembers)
                .HasForeignKey(pm=>pm.TeamRoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectMilestone>()
                .HasOne(pm=>pm.Project)
                .WithMany(p=>p.Milestones)
                .HasForeignKey(pm=>pm.ProjectMilestoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectMilestone>()
                .HasOne(pm=>pm.MilestoneStatus)
                .WithMany(p=>p.Milestones)
                .HasForeignKey(pm=>pm.MilestoneStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estimate>()
                .HasOne(e=>e.Project)
                .WithMany(p=>p.Estimates)
                .HasForeignKey(e=>e.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estimate>()
                .HasOne(e => e.EstimationStatus)
                .WithMany(s => s.Estimates)
                .HasForeignKey(e => e.EstimationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estimate>()
                .HasOne(e=>e.Tax)
                .WithMany(t=>t.Estimates)
                .HasForeignKey(e=>e.TaxId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateLaborCost>()
                .HasOne(el=>el.Estimation)
                .WithMany(e=>e.LaborCosts)
                .HasForeignKey(el=>el.EstimateLaborCostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateLaborCost>()
                .HasOne(el=>el.Employee)
                .WithMany(e=>e.EstimateLaborCosts)
                .HasForeignKey(el=>el.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateDirectCost>()
                .HasOne(el => el.Estimation)
                .WithMany(e => e.DirectCosts)
                .HasForeignKey(el => el.EstimateDirectCostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateDirectCost>()
                .HasOne(ed=>ed.CostType)
                .WithMany(c=>c.DirectCosts)
                .HasForeignKey(ed=>ed.CostTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateIndirectCost>()
                .HasOne(ed => ed.Estimation)
                .WithMany(e => e.IndirectCosts)
                .HasForeignKey(ed => ed.EstimateIndirectCostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateIndirectCost>()
                .HasOne(ed => ed.CostType)
                .WithMany(c => c.IndirectCosts)
                .HasForeignKey(ed => ed.CostTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateAdditionalCost>()
                .HasOne(ea => ea.Estimation)
                .WithMany(e => e.AdditionalCosts)
                .HasForeignKey(ea => ea.EstimateAdditionalCostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstimateAdditionalCost>()
                .HasOne(ed => ed.CostType)
                .WithMany(c => c.AdditionalCosts)
                .HasForeignKey(ed => ed.CostTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i=>i.Project)
                .WithMany(p=>p.Invoices)
                .HasForeignKey(i=>i.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i=>i.Milestone)
                .WithMany(m=>m.Invoices)
                .HasForeignKey(i=>i.MilestoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Client)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.PaymentType)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PaymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.InvoiceStatus)
                .WithMany(s=> s.Invoices)
                .HasForeignKey(i=>i.InvoiceStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentReceipt>()
                .HasOne(p=>p.Invoice)
                .WithMany(i=>i.PaymentReceipts)
                .HasForeignKey(p=>p.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentReceipt>()
                .HasOne(p=>p.PaymentMode)
                .WithMany(pm=>pm.PaymentReceipts)
                .HasForeignKey(p=>p.PaymentModeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAuditLog>()
                .HasOne(cl=> cl.EntityType)
                .WithMany()
                .HasForeignKey(cl=>cl.EntityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAuditLog>()
                .HasOne(cl => cl.ActionType)
                .WithMany()
                .HasForeignKey(cl => cl.ActionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClientAuditLog>()
                .HasOne(cl => cl.PerformedByUser)
                .WithMany()
                .HasForeignKey(cl => cl.PerformedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectAuditLog>()
               .HasOne(cl => cl.EntityType)
               .WithMany()
               .HasForeignKey(cl => cl.EntityTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectAuditLog>()
                .HasOne(cl => cl.ActionType)
                .WithMany()
                .HasForeignKey(cl => cl.ActionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectAuditLog>()
                .HasOne(cl => cl.PerformedByUser)
                .WithMany()
                .HasForeignKey(cl => cl.PerformedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceAuditLog>()
               .HasOne(cl => cl.EntityType)
               .WithMany()
               .HasForeignKey(cl => cl.EntityTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceAuditLog>()
                .HasOne(cl => cl.ActionType)
                .WithMany()
                .HasForeignKey(cl => cl.ActionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceAuditLog>()
                .HasOne(cl => cl.PerformedByUser)
                .WithMany()
                .HasForeignKey(cl => cl.PerformedBy)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
