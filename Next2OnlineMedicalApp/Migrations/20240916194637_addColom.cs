using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Next2OnlineMedicalApp.Migrations
{
    /// <inheritdoc />
    public partial class addColom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "specialization",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specialization",
                table: "Doctors");
        }
    }
}
