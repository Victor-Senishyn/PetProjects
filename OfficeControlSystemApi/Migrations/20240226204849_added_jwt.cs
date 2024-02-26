using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeControlSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class added_jwt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCards_Employees_EmployeeId",
                table: "AccessCards");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AccessCards_AccessCardId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitHistories_AccessCards_AccessCardId",
                table: "VisitHistories");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AccessCardId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitHistories",
                table: "VisitHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessCards",
                table: "AccessCards");

            migrationBuilder.DropColumn(
                name: "AccessCardId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "VisitHistories",
                newName: "visit_history");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "employee");

            migrationBuilder.RenameTable(
                name: "AccessCards",
                newName: "access_card");

            migrationBuilder.RenameIndex(
                name: "IX_VisitHistories_AccessCardId",
                table: "visit_history",
                newName: "IX_visit_history_AccessCardId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessCards_EmployeeId",
                table: "access_card",
                newName: "IX_access_card_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_visit_history",
                table: "visit_history",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employee",
                table: "employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_access_card",
                table: "access_card",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Permission = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    AccessCardId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_access_card_AccessCardId",
                        column: x => x.AccessCardId,
                        principalTable: "access_card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_AccessCardId",
                table: "user",
                column: "AccessCardId");

            migrationBuilder.CreateIndex(
                name: "IX_user_EmployeeId",
                table: "user",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_access_card_employee_EmployeeId",
                table: "access_card",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_visit_history_access_card_AccessCardId",
                table: "visit_history",
                column: "AccessCardId",
                principalTable: "access_card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_card_employee_EmployeeId",
                table: "access_card");

            migrationBuilder.DropForeignKey(
                name: "FK_visit_history_access_card_AccessCardId",
                table: "visit_history");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_visit_history",
                table: "visit_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employee",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_access_card",
                table: "access_card");

            migrationBuilder.RenameTable(
                name: "visit_history",
                newName: "VisitHistories");

            migrationBuilder.RenameTable(
                name: "employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "access_card",
                newName: "AccessCards");

            migrationBuilder.RenameIndex(
                name: "IX_visit_history_AccessCardId",
                table: "VisitHistories",
                newName: "IX_VisitHistories_AccessCardId");

            migrationBuilder.RenameIndex(
                name: "IX_access_card_EmployeeId",
                table: "AccessCards",
                newName: "IX_AccessCards_EmployeeId");

            migrationBuilder.AddColumn<long>(
                name: "AccessCardId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Permission",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitHistories",
                table: "VisitHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessCards",
                table: "AccessCards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AccessCardId",
                table: "AspNetUsers",
                column: "AccessCardId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCards_Employees_EmployeeId",
                table: "AccessCards",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AccessCards_AccessCardId",
                table: "AspNetUsers",
                column: "AccessCardId",
                principalTable: "AccessCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitHistories_AccessCards_AccessCardId",
                table: "VisitHistories",
                column: "AccessCardId",
                principalTable: "AccessCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
