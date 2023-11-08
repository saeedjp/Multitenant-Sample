using Microsoft.EntityFrameworkCore.Migrations;

namespace Multitenant_Sample.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subdomain = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantData_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain" },
                values: new object[] { 1, "Tenant 1", "tenant1" });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name", "Subdomain" },
                values: new object[] { 2, "Tenant 2", "tenant2" });

            migrationBuilder.InsertData(
                table: "TenantData",
                columns: new[] { "Id", "Data", "TenantId" },
                values: new object[] { 1, "Tenant 1 Data", 1 });

            migrationBuilder.InsertData(
                table: "TenantData",
                columns: new[] { "Id", "Data", "TenantId" },
                values: new object[] { 2, "Tenant 2 Data", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_TenantData_TenantId",
                table: "TenantData",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Subdomain",
                table: "Tenants",
                column: "Subdomain",
                unique: true,
                filter: "[Subdomain] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantData");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
