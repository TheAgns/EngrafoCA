using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationHeadingContents",
                columns: table => new
                {
                    DocumentationHeadingContentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationHeadingContents", x => new { x.DocumentationHeadingContentId, x.DocumentationId });
                    table.ForeignKey(
                        name: "FK_DocumentationHeadingContents_Documentations_DocumentationId",
                        column: x => x.DocumentationId,
                        principalTable: "Documentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationHeadingContents_DocumentationId",
                table: "DocumentationHeadingContents",
                column: "DocumentationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentationHeadingContents");

            migrationBuilder.DropTable(
                name: "Documentations");
        }
    }
}
