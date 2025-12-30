using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenXThofa.Technologies.Estimer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_ProjectMilestone_And_ProjectTeamMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeamMembers_Roles_RoleId",
                table: "ProjectTeamMembers");

            migrationBuilder.DropColumn(
                name: "ActualCompletionDate",
                table: "ProjectMilestones");

            migrationBuilder.DropColumn(
                name: "AmountReceived",
                table: "ProjectMilestones");

            migrationBuilder.DropColumn(
                name: "PaymentReceivedDate",
                table: "ProjectMilestones");

            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "ProjectMilestones");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ProjectTeamMembers",
                newName: "RolesRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeamMembers_RoleId",
                table: "ProjectTeamMembers",
                newName: "IX_ProjectTeamMembers_RolesRoleId");

            migrationBuilder.RenameColumn(
                name: "PlannedCompletionDate",
                table: "ProjectMilestones",
                newName: "DueDate");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectMilestones",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeamMembers_Roles_RolesRoleId",
                table: "ProjectTeamMembers",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTeamMembers_Roles_RolesRoleId",
                table: "ProjectTeamMembers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectMilestones");

            migrationBuilder.RenameColumn(
                name: "RolesRoleId",
                table: "ProjectTeamMembers",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTeamMembers_RolesRoleId",
                table: "ProjectTeamMembers",
                newName: "IX_ProjectTeamMembers_RoleId");

            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "ProjectMilestones",
                newName: "PlannedCompletionDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualCompletionDate",
                table: "ProjectMilestones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountReceived",
                table: "ProjectMilestones",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentReceivedDate",
                table: "ProjectMilestones",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "ProjectMilestones",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTeamMembers_Roles_RoleId",
                table: "ProjectTeamMembers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
