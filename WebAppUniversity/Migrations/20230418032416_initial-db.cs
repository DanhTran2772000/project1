using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppUniversity.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Idea",
                table: "Idea");

            migrationBuilder.RenameTable(
                name: "Idea",
                newName: "Ideas");

            migrationBuilder.AlterColumn<int>(
                name: "SumbmissionID",
                table: "Ideas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ideas",
                table: "Ideas");

            migrationBuilder.RenameTable(
                name: "Ideas",
                newName: "Idea");

            migrationBuilder.AlterColumn<string>(
                name: "SumbmissionID",
                table: "Idea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Idea",
                table: "Idea",
                column: "ID");
        }
    }
}
