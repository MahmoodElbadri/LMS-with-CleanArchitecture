using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ID", "Author", "IsBorrowed", "PublishDate", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", false, new DateTime(1925, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Great Gatsby" },
                    { 2, "Harper Lee", false, new DateTime(1960, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "To Kill a Mockingbird" },
                    { 3, "George Orwell", false, new DateTime(1949, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "1984" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "EmailAddress", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "Doe" },
                    { 2, "jane.smith@example.com", "Jane", "Smith" },
                    { 3, "alice.johnson@example.com", "Alice", "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "ID", "BookID", "DueDate", "IsReturned", "LoanDate", "ReturnDate", "UserID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
