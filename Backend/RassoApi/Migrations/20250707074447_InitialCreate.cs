using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace RassoApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "event_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_statuses", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "refusal_reasons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    label = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refusal_reasons", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    latitude = table.Column<double>(type: "double", nullable: true),
                    longitude = table.Column<double>(type: "double", nullable: true),
                    category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    organizer_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    status_id = table.Column<int>(type: "int", nullable: false),
                    moderated_by_id = table.Column<Guid>(type: "char(36)", nullable: true),
                    moderated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    refusal_reason_id = table.Column<int>(type: "int", nullable: true),
                    refusal_comment = table.Column<string>(type: "longtext", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_events_event_statuses_status_id",
                        column: x => x.status_id,
                        principalTable: "event_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_events_refusal_reasons_refusal_reason_id",
                        column: x => x.refusal_reason_id,
                        principalTable: "refusal_reasons",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "event_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    s3_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    filename = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_media_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "event_participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    registered_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_participants", x => x.id);
                    table.ForeignKey(
                        name: "FK_event_participants_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => new { x.user_id, x.event_id });
                    table.ForeignKey(
                        name: "FK_favorites_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_event_media_event_id",
                table: "event_media",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_event_participants_event_id",
                table: "event_participants",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_refusal_reason_id",
                table: "events",
                column: "refusal_reason_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_status_id",
                table: "events",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_event_id",
                table: "favorites",
                column: "event_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_media");

            migrationBuilder.DropTable(
                name: "event_participants");

            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "event_statuses");

            migrationBuilder.DropTable(
                name: "refusal_reasons");
        }
    }
}
