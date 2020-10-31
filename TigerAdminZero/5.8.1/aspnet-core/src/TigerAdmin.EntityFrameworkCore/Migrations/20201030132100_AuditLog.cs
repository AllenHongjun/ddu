using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TigerAdmin.Migrations
{
    public partial class AuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomCssId",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsInTrialPeriod",
                table: "AbpTenants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LogoFileType",
                table: "AbpTenants",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LogoId",
                table: "AbpTenants",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDateUtc",
                table: "AbpTenants",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomCssId",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "IsInTrialPeriod",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "LogoFileType",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDateUtc",
                table: "AbpTenants");
        }
    }
}
