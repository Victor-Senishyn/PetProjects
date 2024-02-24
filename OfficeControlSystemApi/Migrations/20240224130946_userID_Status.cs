using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeControlSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class userID_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Employees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Employees");
        }
    }
}
