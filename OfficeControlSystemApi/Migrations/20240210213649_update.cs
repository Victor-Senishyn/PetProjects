using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeControlSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCards_Employees_EmployeeId",
                table: "AccessCards");

            migrationBuilder.DropIndex(
                name: "IX_AccessCards_EmployeeId",
                table: "AccessCards");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AccessCards_EmployeeId",
                table: "AccessCards",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCards_Employees_EmployeeId",
                table: "AccessCards",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
