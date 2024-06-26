﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "DocumentationTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationItems",
                columns: table => new
                {
                    DocumentationItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationItems", x => new { x.DocumentationItemId, x.DocumentationId });
                    table.ForeignKey(
                        name: "FK_DocumentationItems_Documentations_DocumentationId",
                        column: x => x.DocumentationId,
                        principalTable: "Documentations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentationTemplateHeadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentationTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentationTemplateHeadings", x => new { x.Id, x.DocumentationTemplateId });
                    table.ForeignKey(
                        name: "FK_DocumentationTemplateHeadings_DocumentationTemplates_DocumentationTemplateId",
                        column: x => x.DocumentationTemplateId,
                        principalTable: "DocumentationTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DocumentationTemplates",
                columns: new[] { "Id", "Title" },
                values: new object[] { new Guid("f843bcd9-57ed-46aa-b65b-fcaeb1eb9628"), "Template1" });

            migrationBuilder.InsertData(
                table: "DocumentationTemplateHeadings",
                columns: new[] { "DocumentationTemplateId", "Id", "Position", "Title" },
                values: new object[,]
                {
                    { new Guid("f843bcd9-57ed-46aa-b65b-fcaeb1eb9628"), 1, 0, "Heading1" },
                    { new Guid("f843bcd9-57ed-46aa-b65b-fcaeb1eb9628"), 2, 1, "Heading2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationItems_DocumentationId",
                table: "DocumentationItems",
                column: "DocumentationId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentationTemplateHeadings_DocumentationTemplateId",
                table: "DocumentationTemplateHeadings",
                column: "DocumentationTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentationItems");

            migrationBuilder.DropTable(
                name: "DocumentationTemplateHeadings");

            migrationBuilder.DropTable(
                name: "Documentations");

            migrationBuilder.DropTable(
                name: "DocumentationTemplates");
        }
    }
}
