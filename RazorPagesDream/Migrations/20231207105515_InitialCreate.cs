using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesDream.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DreamVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DreamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DreamText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadableBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DreamVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DreamVM");
        }
    }
}
