using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heyday.Infrastructure.Migrations
{
    public partial class add_manager_id_to_schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_schedules_manager_id",
                table: "schedules",
                column: "manager_id");

            migrationBuilder.AddForeignKey(
                name: "FK_schedules_users_manager_id",
                table: "schedules",
                column: "manager_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_schedules_users_manager_id",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "IX_schedules_manager_id",
                table: "schedules");
        }
    }
}
