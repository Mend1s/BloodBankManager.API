using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodBankManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class retirandoIdAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Id",
                table: "Donors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Address_Id",
                table: "Donors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
