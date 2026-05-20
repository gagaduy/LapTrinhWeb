using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buoi5.Migrations
{
    /// <inheritdoc />
    public partial class KhoiTaoBookDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Cuoc song" },
                    { 2, "Lap trinh" },
                    { 3, "Suc Khoe" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Title", "Author", "Price", "Description", "Image", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Cho toi xin mot ve di tuoi tho", "Nguyen Nhat Anh", 75000m, "Tac pham noi tieng cua Nguyen Nhat Anh, mang mau sac trong treo va goi nho ve nhung ky niem tuoi tho than thuong.", "cho-toi-xin-mot-ve-di-tuoi-tho.svg", 1 },
                    { 2, "Lap trinh C#", "Tac gia TL Xuan Viet", 92000m, "Sach tong hop kien thuc nhap mon va thuc hanh ngon ngu C#, phu hop cho sinh vien bat dau hoc lap trinh huong doi tuong.", "lap-trinh-csharp.svg", 2 },
                    { 3, "Core Java: Fundamentals, Volume 1", "Cay Horstmann", 135000m, "Tai lieu co ban ve Java, trinh bay cac khai niem cot loi, cau truc ngon ngu va cach ung dung trong bai toan thuc te.", "core-java.svg", 2 },
                    { 4, "Cuoc Song Rat Giong Cuoc Doi", "Hai Do", 61000m, "Cuon sach truyen cam hung ve hanh trinh truong thanh, nhin cuoc song bang goc nhin tich cuc va thuc te hon moi ngay.", "cuoc-song-rat-giong-cuoc-doi.svg", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
