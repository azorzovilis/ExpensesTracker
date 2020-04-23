using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesTrackerAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseType",
                columns: table => new
                {
                    ExpenseTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseType", x => x.ExpenseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Recipient = table.Column<string>(maxLength: 100, nullable: false),
                    Currency = table.Column<string>(maxLength: 3, nullable: false),
                    ExpenseTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expense_ExpenseType_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseType",
                        principalColumn: "ExpenseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpenseType",
                columns: new[] { "ExpenseTypeId", "Description" },
                values: new object[] { 0, "Other" });

            migrationBuilder.InsertData(
                table: "ExpenseType",
                columns: new[] { "ExpenseTypeId", "Description" },
                values: new object[] { 1, "Food" });

            migrationBuilder.InsertData(
                table: "ExpenseType",
                columns: new[] { "ExpenseTypeId", "Description" },
                values: new object[] { 2, "Drinks" });

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "ExpenseId", "Amount", "Currency", "ExpenseTypeId", "Recipient", "TransactionDate" },
                values: new object[,]
                {
                    { 3, 35.88m, "EUR", 0, "Artemis", new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 56.16m, "EUR", 0, "Glen", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 54.12m, "EUR", 0, "Caren", new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 10.4m, "GBP", 1, "Alex", new DateTime(2020, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 105.22m, "GBP", 1, "Thomas", new DateTime(2020, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 44.17m, "JPY", 1, "John", new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 182.59m, "AUD", 1, "Patricia", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 8.25m, "EUR", 1, "Maria", new DateTime(2020, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3.99m, "USD", 1, "Paul", new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5.6m, "CHF", 2, "Eliza", new DateTime(2020, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 25.00m, "CHF", 2, "Rick", new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 98.99m, "CHF", 2, "George", new DateTime(2020, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseTypeId",
                table: "Expense",
                column: "ExpenseTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "ExpenseType");
        }
    }
}
