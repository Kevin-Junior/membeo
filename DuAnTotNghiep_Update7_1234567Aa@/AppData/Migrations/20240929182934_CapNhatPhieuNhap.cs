using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class CapNhatPhieuNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "NgayXuatHang",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "TenNguoiXuat",
                table: "PhieuXuat");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "NgayNhanHang",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "TenNguoiNhan",
                table: "PhieuNhap");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "PhieuXuat",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "PhieuNhap",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "PhieuXuat",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "PhieuXuat",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PhieuXuat",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXuatHang",
                table: "PhieuXuat",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "PhieuXuat",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiXuat",
                table: "PhieuXuat",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "PhieuNhap",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "PhieuNhap",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PhieuNhap",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayNhanHang",
                table: "PhieuNhap",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "PhieuNhap",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenNguoiNhan",
                table: "PhieuNhap",
                type: "nvarchar(100)",
                nullable: true);
        }
    }
}
