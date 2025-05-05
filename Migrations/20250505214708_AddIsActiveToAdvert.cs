using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace naboo_final_project.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToAdvert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Adverts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Adverts");
        }
    }
}
