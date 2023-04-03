using Microsoft.EntityFrameworkCore.Migrations;

namespace BeforeTheScholarship.Context.Migrations.PostgreSQL.Migrations
{
    public partial class DbRemoveDebtsEmailSendedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSended",
                table: "Debts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailSended",
                table: "Debts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
