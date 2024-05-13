using System;
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Documentations",
                columns: new[] { "Id", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("0dcf6968-b5f3-4577-81fd-2fbc3bd84ba4"), new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Local).AddTicks(2603), null, new DateTimeOffset(new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Unspecified).AddTicks(2605), new TimeSpan(0, 2, 0, 0, 0)), null, "Doc3" },
                    { new Guid("cec2f141-3f12-4e61-881c-84eb3ff8bf82"), new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Local).AddTicks(2500), null, new DateTimeOffset(new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Unspecified).AddTicks(2562), new TimeSpan(0, 2, 0, 0, 0)), null, "Doc1" },
                    { new Guid("f8336276-b4f3-418f-9057-8bfe05748a10"), new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Local).AddTicks(2597), null, new DateTimeOffset(new DateTime(2024, 5, 13, 0, 10, 7, 426, DateTimeKind.Unspecified).AddTicks(2599), new TimeSpan(0, 2, 0, 0, 0)), null, "Doc2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentations");
        }
    }
}
