using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sippedes.Migrations
{
    /// <inheritdoc />
    public partial class otp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_otp",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uniqueidentifier", nullable: false),
                    otpcode = table.Column<int>(name: "otp_code", type: "int", nullable: false),
                    isexpired = table.Column<short>(name: "is_expired", type: "smallint", nullable: false),
                    lastexpiration = table.Column<DateTime>(name: "last_expiration", type: "datetime2", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_otp", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_otp_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_otp_UserId",
                table: "m_otp",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_otp");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
