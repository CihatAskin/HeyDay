using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heyday.Infrastructure.Migrations
{
    public partial class suitableHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "suitable_hours",
                table: "user_schedule");

            migrationBuilder.AddColumn<List<short>>(
                name: "suitable_hour_keys",
                table: "user_schedule",
                type: "smallint[]",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "schedules",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "decided_hour_key",
                table: "schedules",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_canceled",
                table: "schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "suitable_hour_keys",
                table: "user_schedule");

            migrationBuilder.DropColumn(
                name: "decided_hour_key",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "description",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "is_canceled",
                table: "schedules");

            migrationBuilder.AddColumn<string>(
                name: "suitable_hours",
                table: "user_schedule",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date",
                table: "schedules",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
