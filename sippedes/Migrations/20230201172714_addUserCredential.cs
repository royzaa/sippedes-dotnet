using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sippedes.Migrations
{
    /// <inheritdoc />
    public partial class addUserCredential : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_user_credential",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    roleid = table.Column<Guid>(name: "role_id", type: "uniqueidentifier", nullable: false),
                    isverifed = table.Column<int>(name: "is_verifed", type: "int", nullable: true),
                    civildataid = table.Column<string>(name: "civil_data_id", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_user_credential", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_user_credential_m_civil_data_civil_data_id",
                        column: x => x.civildataid,
                        principalTable: "m_civil_data",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_m_user_credential_m_role_role_id",
                        column: x => x.roleid,
                        principalTable: "m_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "m_admin_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(max)", nullable: false),
                    isactive = table.Column<int>(name: "is_active", type: "int", nullable: false),
                    usercredentialid = table.Column<Guid>(name: "user_credential_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_admin_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_admin_data_m_user_credential_user_credential_id",
                        column: x => x.usercredentialid,
                        principalTable: "m_user_credential",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_admin_data_user_credential_id",
                table: "m_admin_data",
                column: "user_credential_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_civil_data_id",
                table: "m_user_credential",
                column: "civil_data_id");

            migrationBuilder.CreateIndex(
                name: "IX_m_user_credential_role_id",
                table: "m_user_credential",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_admin_data");

            migrationBuilder.DropTable(
                name: "m_user_credential");

            migrationBuilder.DropTable(
                name: "m_role");
        }
    }
}
