using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lamiapizzeriastatic.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewPizza : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PizzaId",
                table: "Reviews",
                column: "PizzaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Pizze_PizzaId",
                table: "Reviews",
                column: "PizzaId",
                principalTable: "Pizze",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Pizze_PizzaId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PizzaId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PizzaId",
                table: "Reviews");
        }
    }
}
