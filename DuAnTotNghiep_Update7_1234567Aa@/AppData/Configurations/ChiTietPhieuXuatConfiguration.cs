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
    public class ChiTietPhieuXuatConfiguration : IEntityTypeConfiguration<ChiTietPhieuXuat>
    {
        public void Configure(EntityTypeBuilder<ChiTietPhieuXuat> builder)
        {
            builder.ToTable("ChiTietPhieuXuat");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.SoLuong).HasColumnType("int");
            builder.HasOne(x => x.PhieuXuat).WithMany(x => x.ChiTietPhieuXuats).HasForeignKey(x => x.IDPhieuXuat);
            builder.HasOne(x => x.ChiTietSanPham).WithMany(x => x.ChiTietPhieuXuats).HasForeignKey(x => x.IDCTSP);
        }
    }
}
