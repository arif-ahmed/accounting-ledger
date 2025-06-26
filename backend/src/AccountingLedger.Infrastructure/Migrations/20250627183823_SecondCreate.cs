using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingLedger.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JournalEntryId1",
                table: "JournalEntryLines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalEntryLines_JournalEntryId1",
                table: "JournalEntryLines",
                column: "JournalEntryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLines_Accounts_AccountId",
                table: "JournalEntryLines",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalEntryLines_JournalEntries_JournalEntryId1",
                table: "JournalEntryLines",
                column: "JournalEntryId1",
                principalTable: "JournalEntries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLines_Accounts_AccountId",
                table: "JournalEntryLines");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalEntryLines_JournalEntries_JournalEntryId1",
                table: "JournalEntryLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryLines_AccountId",
                table: "JournalEntryLines");

            migrationBuilder.DropIndex(
                name: "IX_JournalEntryLines_JournalEntryId1",
                table: "JournalEntryLines");

            migrationBuilder.DropColumn(
                name: "JournalEntryId1",
                table: "JournalEntryLines");
        }
    }
}
