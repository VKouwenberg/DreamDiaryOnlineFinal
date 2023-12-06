using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcDream.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DreamViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DreamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DreamText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadableBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DreamerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamViewModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DreamViewModel");
        }
    }
}
