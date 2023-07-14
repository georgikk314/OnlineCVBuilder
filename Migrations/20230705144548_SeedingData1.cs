using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_CV_Builder.Migrations
{
    public partial class SeedingData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "TemplateFilePath", "TemplateName" },
                values: new object[] { 1, "C:\\Online CV Builder\\Online CV Builder", "CoolTemplate" });

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "TemplateFilePath", "TemplateName" },
                values: new object[] { 2, "C:\\Online CV Builder\\Online CV Builder", "VeryCoolTemplate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
