﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeforeTheScholarship.Context.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class DbRemoveDebtsEmailSendedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSended",
                table: "Debts");
        }

        /// <inheritdoc />
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