using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BellSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    Imei = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: true),
                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    Lob = table.Column<string>(type: "TEXT", nullable: true),
                    SubLob = table.Column<string>(type: "TEXT", nullable: true),
                    RebateType = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                    MatchStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BellSources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaplesSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    OrderNumber = table.Column<long>(type: "INTEGER", nullable: true),
                    RebateType = table.Column<string>(type: "TEXT", nullable: true),
                    Product = table.Column<string>(type: "TEXT", nullable: true),
                    Rec = table.Column<string>(type: "TEXT", nullable: true),
                    Imei = table.Column<string>(type: "TEXT", nullable: true),
                    TransactionDate = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPerson = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    TaxCode = table.Column<long>(type: "INTEGER", nullable: true),
                    Msf = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceCo = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Brand = table.Column<string>(type: "TEXT", nullable: true),
                    SubLob = table.Column<string>(type: "TEXT", nullable: true),
                    ReconciledBy = table.Column<string>(type: "TEXT", nullable: true),
                    MatchStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaplesSources", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BellSources");

            migrationBuilder.DropTable(
                name: "StaplesSources");
        }
    }
}
