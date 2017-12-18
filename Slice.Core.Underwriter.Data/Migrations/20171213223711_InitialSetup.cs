using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Slice.Core.Underwriter.Data.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "weather");

            migrationBuilder.CreateTable(
                name: "override",
                schema: "weather",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    area = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    created_by_id = table.Column<int>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<Guid>(nullable: true),
                    ends_on = table.Column<DateTime>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    modified_by_id = table.Column<int>(nullable: true),
                    modified_on = table.Column<DateTime>(nullable: true),
                    override_type = table.Column<int>(nullable: false),
                    starts_on = table.Column<DateTime>(nullable: false),
                    warning_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_override", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warnings",
                schema: "weather",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    area = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    created_by_id = table.Column<int>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: true),
                    deleted_by = table.Column<Guid>(nullable: true),
                    ends_on = table.Column<DateTime>(nullable: false),
                    is_deleted = table.Column<bool>(nullable: false),
                    modified_by_id = table.Column<int>(nullable: true),
                    modified_on = table.Column<DateTime>(nullable: true),
                    searched_on = table.Column<DateTime>(nullable: false),
                    starts_on = table.Column<DateTime>(nullable: false),
                    warning_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warnings", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "override",
                schema: "weather");

            migrationBuilder.DropTable(
                name: "warnings",
                schema: "weather");
        }
    }
}
