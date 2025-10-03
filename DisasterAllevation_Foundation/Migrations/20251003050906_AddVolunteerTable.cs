using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisasterAlleviation_Foundation.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedDisasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Disasters_AssignedDisasterId",
                        column: x => x.AssignedDisasterId,
                        principalTable: "Disasters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_AssignedDisasterId",
                table: "Volunteers",
                column: "AssignedDisasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteers");
        }
    }
}
