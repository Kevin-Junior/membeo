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
    public class PhieuXuatConfiguration : IEntityTypeConfiguration<PhieuXuat>
    {
        public void Configure(EntityTypeBuilder<PhieuXuat> builder)
        {
            builder.ToTable("PhieuXuat");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.NgayTao).HasColumnType("datetime");
            builder.Property(x => x.GhiChu).HasColumnType("nvarchar(100)");
            builder.HasOne(x => x.NhanVien).WithMany(x => x.PhieuXuats).HasForeignKey(x => x.IDNhanVien);
            builder.HasOne(x => x.KhoHang).WithMany(x => x.PhieuXuats).HasForeignKey(x => x.IDKhoHang);
        }
    }
}
