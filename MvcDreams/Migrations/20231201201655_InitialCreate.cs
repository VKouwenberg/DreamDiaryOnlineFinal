using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcDreams.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dream",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadableBy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DreamText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dream", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dream");
        }
    }
}
