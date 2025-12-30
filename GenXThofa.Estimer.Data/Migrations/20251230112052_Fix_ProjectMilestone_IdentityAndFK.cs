using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenXThofa.Technologies.Estimer.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fix_ProjectMilestone_IdentityAndFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestones_Projects_ProjectMilestoneId",
                table: "ProjectMilestones");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMilestones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectMilestoneId",
                table: "ProjectMilestones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMilestones_ProjectId",
                table: "ProjectMilestones",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestones_Projects_ProjectId",
                table: "ProjectMilestones",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMilestones_Projects_ProjectId",
                table: "ProjectMilestones");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMilestones_ProjectId",
                table: "ProjectMilestones");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectMilestones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectMilestoneId",
                table: "ProjectMilestones",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMilestones_Projects_ProjectMilestoneId",
                table: "ProjectMilestones",
                column: "ProjectMilestoneId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
