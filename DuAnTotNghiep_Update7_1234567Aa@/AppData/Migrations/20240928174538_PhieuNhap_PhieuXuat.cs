using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class PhieuNhap_PhieuXuat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhieuNhap",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaPN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayNhanHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IDNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDKhoHang = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuNhap", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhieuNhap_KhoHang_IDKhoHang",
                        column: x => x.IDKhoHang,
                        principalTable: "KhoHang",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PhieuNhap_NhanVien_IDNhanVien",
                        column: x => x.IDNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PhieuXuat",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaPX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayXuatHang = table.Column<DateTime>(type: "datetime", nullable: true),
                    TenNguoiXuat = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IDNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDKhoHang = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuXuat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PhieuXuat_KhoHang_IDKhoHang",
                        column: x => x.IDKhoHang,
                        principalTable: "KhoHang",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PhieuXuat_NhanVien_IDNhanVien",
                        column: x => x.IDNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuNhap",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    IDCTSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDPhieuNhap = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuNhap", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_ChiTietSanPham_IDCTSP",
                        column: x => x.IDCTSP,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuNhap_PhieuNhap_IDPhieuNhap",
                        column: x => x.IDPhieuNhap,
                        principalTable: "PhieuNhap",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuXuat",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    IDCTSP = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDPhieuXuat = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuXuat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuXuat_ChiTietSanPham_IDCTSP",
                        column: x => x.IDCTSP,
                        principalTable: "ChiTietSanPham",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuXuat_PhieuXuat_IDPhieuXuat",
                        column: x => x.IDPhieuXuat,
                        principalTable: "PhieuXuat",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_IDCTSP",
                table: "ChiTietPhieuNhap",
                column: "IDCTSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuNhap_IDPhieuNhap",
                table: "ChiTietPhieuNhap",
                column: "IDPhieuNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_IDCTSP",
                table: "ChiTietPhieuXuat",
                column: "IDCTSP");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuXuat_IDPhieuXuat",
                table: "ChiTietPhieuXuat",
                column: "IDPhieuXuat");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDKhoHang",
                table: "PhieuNhap",
                column: "IDKhoHang");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDNhanVien",
                table: "PhieuNhap",
                column: "IDNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_IDKhoHang",
                table: "PhieuXuat",
                column: "IDKhoHang");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuXuat_IDNhanVien",
                table: "PhieuXuat",
                column: "IDNhanVien");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietPhieuNhap");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuXuat");

            migrationBuilder.DropTable(
                name: "PhieuNhap");

            migrationBuilder.DropTable(
                name: "PhieuXuat");
        }
    }
}
