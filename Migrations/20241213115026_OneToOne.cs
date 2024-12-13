using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tenants.Migrations
{
    /// <inheritdoc />
    public partial class OneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Properties_TenantOfId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_TenantOfId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TenantOfId",
                table: "Tenants");

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Tenants",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants",
                column: "PropertyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Properties_PropertyId",
                table: "Tenants",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Properties_PropertyId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PropertyId",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Tenants");

            migrationBuilder.AddColumn<int>(
                name: "TenantOfId",
                table: "Tenants",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantOfId",
                table: "Tenants",
                column: "TenantOfId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Properties_TenantOfId",
                table: "Tenants",
                column: "TenantOfId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
