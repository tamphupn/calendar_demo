using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarDemo.Migrations
{
    public partial class update_event_time : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "Invitations");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateFinish",
                table: "Invitations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDateStart",
                table: "Invitations",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Username" },
                values: new object[] { new Guid("7ab7790d-e111-4b70-83ae-1d3e15d8a3e1"), "tamphu.pn@gmail.com", "12345678", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7ab7790d-e111-4b70-83ae-1d3e15d8a3e1"));

            migrationBuilder.DropColumn(
                name: "EventDateFinish",
                table: "Invitations");

            migrationBuilder.DropColumn(
                name: "EventDateStart",
                table: "Invitations");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "Invitations",
                type: "datetime2",
                nullable: true);
        }
    }
}
