using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Organizer.Database.Storage.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLoanAndBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepaymentAt",
                table: "Loans",
                newName: "NextRepayment");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ApartmentNumber",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuildingNumber",
                table: "Apartments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "ApartmentNumber",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "BuildingNumber",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Apartments");

            migrationBuilder.RenameColumn(
                name: "NextRepayment",
                table: "Loans",
                newName: "RepaymentAt");
        }
    }
}
