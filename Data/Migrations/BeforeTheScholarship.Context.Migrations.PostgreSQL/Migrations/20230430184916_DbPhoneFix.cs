using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeforeTheScholarship.Context.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class DbPhoneFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StudentUsers",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true,
                oldDefaultValue: " ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StudentUsers",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                defaultValue: " ",
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);
        }
    }
}
