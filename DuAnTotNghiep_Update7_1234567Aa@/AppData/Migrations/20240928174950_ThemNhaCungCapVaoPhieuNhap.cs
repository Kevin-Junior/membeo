using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class ThemNhaCungCapVaoPhieuNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IDNhaCungCap",
                table: "PhieuNhap",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuNhap_IDNhaCungCap",
                table: "PhieuNhap",
                column: "IDNhaCungCap");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuNhap_NhaCungCap_IDNhaCungCap",
                table: "PhieuNhap",
                column: "IDNhaCungCap",
                principalTable: "NhaCungCap",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuNhap_NhaCungCap_IDNhaCungCap",
                table: "PhieuNhap");

            migrationBuilder.DropIndex(
                name: "IX_PhieuNhap_IDNhaCungCap",
                table: "PhieuNhap");

            migrationBuilder.DropColumn(
                name: "IDNhaCungCap",
                table: "PhieuNhap");
        }
    }
}
