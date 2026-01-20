using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class Update_Email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IDCTSP",
                table: "SanPhamYeuThichs",
                newName: "IDSP");

            migrationBuilder.AddColumn<bool>(
                name: "DaKichHoat",
                table: "KhachHang",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "IDKhachHang",
                table: "HoaDon",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThichs_IDSP",
                table: "SanPhamYeuThichs",
                column: "IDSP");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IDKhachHang",
                table: "HoaDon",
                column: "IDKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_KhachHang_IDKhachHang",
                table: "HoaDon",
                column: "IDKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IDKhachHang");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamYeuThichs_SanPham_IDSP",
                table: "SanPhamYeuThichs",
                column: "IDSP",
                principalTable: "SanPham",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_KhachHang_IDKhachHang",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamYeuThichs_SanPham_IDSP",
                table: "SanPhamYeuThichs");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamYeuThichs_IDSP",
                table: "SanPhamYeuThichs");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_IDKhachHang",
                table: "HoaDon");

            migrationBuilder.DropColumn(
                name: "DaKichHoat",
                table: "KhachHang");

            migrationBuilder.DropColumn(
                name: "IDKhachHang",
                table: "HoaDon");

            migrationBuilder.RenameColumn(
                name: "IDSP",
                table: "SanPhamYeuThichs",
                newName: "IDCTSP");
        }
    }
}
