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
    public class PhieuNhapConfiguration : IEntityTypeConfiguration<PhieuNhap>
    {
        public void Configure(EntityTypeBuilder<PhieuNhap> builder)
        {
            builder.ToTable("PhieuNhap");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.NgayTao).HasColumnType("datetime");
            builder.Property(x => x.GhiChu).HasColumnType("nvarchar(100)");
            builder.HasOne(x => x.NhanVien).WithMany(x => x.PhieuNhaps).HasForeignKey(x => x.IDNhanVien);
            builder.HasOne(x => x.KhoHang).WithMany(x => x.PhieuNhaps).HasForeignKey(x => x.IDKhoHang);
            builder.HasOne(x => x.NhaCungCap).WithMany(x => x.PhieuNhaps).HasForeignKey(x => x.IDNhaCungCap);
        }
    }
}
