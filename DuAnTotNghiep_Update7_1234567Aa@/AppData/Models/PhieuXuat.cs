using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class PhieuXuat
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Mã phiếu xuất không được để trống !")]
        public string MaPX { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? GhiChu { get; set; }
        public Guid? IDNhanVien { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        public Guid? IDKhoHang { get; set; }
        public virtual KhoHang? KhoHang { get; set; }
        public virtual List<ChiTietPhieuXuat>? ChiTietPhieuXuats { get; set; }
    }
}
