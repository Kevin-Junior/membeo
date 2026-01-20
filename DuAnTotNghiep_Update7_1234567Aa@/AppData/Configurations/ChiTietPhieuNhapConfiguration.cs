using AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configurations
{
    public class ChiTietPhieuNhapConfiguration : IEntityTypeConfiguration<ChiTietPhieuNhap>
    {
        public void Configure(EntityTypeBuilder<ChiTietPhieuNhap> builder)
        {
            builder.ToTable("ChiTietPhieuNhap");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.HasOne(x => x.PhieuNhap).WithMany(x => x.ChiTietPhieuNhaps).HasForeignKey(x => x.IDPhieuNhap);
            builder.HasOne(x => x.ChiTietSanPham).WithMany(x => x.ChiTietPhieuNhaps).HasForeignKey(x => x.IDCTSP);
        }
    }
}
