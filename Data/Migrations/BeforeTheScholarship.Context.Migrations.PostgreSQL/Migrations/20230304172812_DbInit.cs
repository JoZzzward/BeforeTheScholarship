using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeforeTheScholarship.Context.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentUsersRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Borrowed = table.Column<decimal>(type: "numeric", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    BorrowedFromWho = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    EmailSended = table.Column<bool>(type: "boolean", nullable: false),
                    WhenBorrowed = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    WhenToPayback = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debts_StudentUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUsersClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentUsersClaims_StudentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "StudentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUsersLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_StudentUsersLogins_StudentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "StudentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUsersTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_StudentUsersTokens_StudentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "StudentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUserRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUserRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentUserRoleClaims_StudentUsersRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StudentUsersRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentUsersRoleOwners",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentUsersRoleOwners", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_StudentUsersRoleOwners_StudentUsersRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "StudentUsersRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentUsersRoleOwners_StudentUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "StudentUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Debts_StudentId",
                table: "Debts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_Uid",
                table: "Debts",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUserRoleClaims_RoleId",
                table: "StudentUserRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "StudentUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_Email",
                table: "StudentUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsers_PhoneNumber",
                table: "StudentUsers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "StudentUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsersClaims_UserId",
                table: "StudentUsersClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsersLogins_UserId",
                table: "StudentUsersLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentUsersRoleOwners_RoleId",
                table: "StudentUsersRoleOwners",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "StudentUsersRoles",
                column: "NormalizedName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.DropTable(
                name: "StudentUserRoleClaims");

            migrationBuilder.DropTable(
                name: "StudentUsersClaims");

            migrationBuilder.DropTable(
                name: "StudentUsersLogins");

            migrationBuilder.DropTable(
                name: "StudentUsersRoleOwners");

            migrationBuilder.DropTable(
                name: "StudentUsersTokens");

            migrationBuilder.DropTable(
                name: "StudentUsersRoles");

            migrationBuilder.DropTable(
                name: "StudentUsers");
        }
    }
}
