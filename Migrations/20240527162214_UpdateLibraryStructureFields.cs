using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryTRY3.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLibraryStructureFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Accepted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooksDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Taken = table.Column<bool>(type: "INTEGER", nullable: false),
                    TakenBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Author = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Publisher = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    YearOfPublication = table.Column<int>(type: "INTEGER", nullable: false),
                    UserAdminAdded = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksDetails", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "BooksDetails");
        }
    }
}
