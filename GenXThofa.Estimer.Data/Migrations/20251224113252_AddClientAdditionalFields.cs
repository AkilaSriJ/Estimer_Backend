using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenXThofa.Technologies.Estimer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddClientAdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Clients",
                newName: "StateProvince");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "PostalCode");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompanyContactPerson",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clients",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CompanyContactPerson",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "StateProvince",
                table: "Clients",
                newName: "ClientName");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Clients",
                newName: "Address");
        }
    }
}
