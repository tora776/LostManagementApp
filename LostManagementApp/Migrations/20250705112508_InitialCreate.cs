using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LostManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    login_password = table.Column<string>(type: "text", nullable: false),
                    registrate_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "login_check",
                columns: table => new
                {
                    login_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    login_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    expire_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_check", x => x.login_id);
                    table.ForeignKey(
                        name: "FK_login_check_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "losts",
                columns: table => new
                {
                    lost_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    is_found = table.Column<bool>(type: "boolean", nullable: false),
                    lost_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    found_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lost_item = table.Column<string>(type: "text", nullable: false),
                    lost_place = table.Column<string>(type: "text", nullable: false),
                    lost_detailed_place = table.Column<string>(type: "text", nullable: false),
                    registrate_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_losts", x => x.lost_id);
                    table.ForeignKey(
                        name: "FK_losts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_check_user_id",
                table: "login_check",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_losts_user_id",
                table: "losts",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_check");

            migrationBuilder.DropTable(
                name: "losts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
