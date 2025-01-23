using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalanceMaster.SqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class OverdraftTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Overdrafts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overdrafts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Overdrafts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(@"
INSERT INTO dbo.Overdrafts (AccountId, Amount, StartDate, EndDate )
SELECT Id, Overdraft_Amount, Overdraft_StartDate, Overdraft_EndDate
FROM  dbo.Accounts
WHERE Overdraft_Amount IS NOT NULL");

            migrationBuilder.DropColumn(
                name: "Overdraft_Amount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Overdraft_StartDate",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Overdraft_EndDate",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Overdraft_Amount",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Overdraft_StartDate",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Overdraft_EndDate",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql(@"
UPDATE Accounts
SET
    Overdraft_Amount = o.Amount,
    Overdraft_StartDate = o.StartDate,
    Overdraft_EndDate = o.EndDate
FROM Accounts a
INNER JOIN Overdrafts o ON a.Id = o.AccountId;");

            migrationBuilder.DropTable(
                name: "Overdrafts");
        }
    }
}