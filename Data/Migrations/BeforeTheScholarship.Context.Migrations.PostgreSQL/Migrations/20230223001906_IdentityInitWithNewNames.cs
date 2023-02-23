using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeforeTheScholarship.Context.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class IdentityInitWithNewNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_StudentUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_StudentUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_StudentUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_StudentUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "StudentUsersTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "StudentUsersRoleOwners");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "StudentUsersLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "StudentUsersClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "StudentUserRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "StudentUsersRoleOwners",
                newName: "IX_StudentUsersRoleOwners_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "StudentUsersLogins",
                newName: "IX_StudentUsersLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "StudentUsersClaims",
                newName: "IX_StudentUsersClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "StudentUserRoleClaims",
                newName: "IX_StudentUserRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUsersTokens",
                table: "StudentUsersTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUsersRoleOwners",
                table: "StudentUsersRoleOwners",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUsersLogins",
                table: "StudentUsersLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUsersClaims",
                table: "StudentUsersClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentUserRoleClaims",
                table: "StudentUserRoleClaims",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "StudentUsersRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersRoles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUserRoleClaims_AspNetRoles_RoleId",
                table: "StudentUserRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsersClaims_StudentUsers_UserId",
                table: "StudentUsersClaims",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsersLogins_StudentUsers_UserId",
                table: "StudentUsersLogins",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsersRoleOwners_AspNetRoles_RoleId",
                table: "StudentUsersRoleOwners",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsersRoleOwners_StudentUsers_UserId",
                table: "StudentUsersRoleOwners",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentUsersTokens_StudentUsers_UserId",
                table: "StudentUsersTokens",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentUserRoleClaims_AspNetRoles_RoleId",
                table: "StudentUserRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsersClaims_StudentUsers_UserId",
                table: "StudentUsersClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsersLogins_StudentUsers_UserId",
                table: "StudentUsersLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsersRoleOwners_AspNetRoles_RoleId",
                table: "StudentUsersRoleOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsersRoleOwners_StudentUsers_UserId",
                table: "StudentUsersRoleOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentUsersTokens_StudentUsers_UserId",
                table: "StudentUsersTokens");

            migrationBuilder.DropTable(
                name: "StudentUsersRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUsersTokens",
                table: "StudentUsersTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUsersRoleOwners",
                table: "StudentUsersRoleOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUsersLogins",
                table: "StudentUsersLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUsersClaims",
                table: "StudentUsersClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentUserRoleClaims",
                table: "StudentUserRoleClaims");

            migrationBuilder.RenameTable(
                name: "StudentUsersTokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "StudentUsersRoleOwners",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "StudentUsersLogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "StudentUsersClaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "StudentUserRoleClaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUsersRoleOwners_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUsersLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUsersClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentUserRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_StudentUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_StudentUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_StudentUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_StudentUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "StudentUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
