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
    public class KhoHangConfiguration : IEntityTypeConfiguration<KhoHang>
    {
        public void Configure(EntityTypeBuilder<KhoHang> builder)
        {
            builder.ToTable("KhoHang");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Ten).HasColumnType("nvarchar(200)");
            builder.Property(x => x.DiaChi).HasColumnType("nvarchar(250)");
        }
    }
}
