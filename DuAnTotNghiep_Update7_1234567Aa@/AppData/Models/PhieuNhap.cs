using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class PhieuNhap
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Mã phiếu nhập không được để trống")]
        public string MaPN { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? GhiChu { get; set; }
        public Guid? IDNhanVien { get; set; }
        public virtual NhanVien? NhanVien { get; set; }
        public Guid? IDKhoHang {  get; set; }
        public virtual KhoHang? KhoHang { get; set; }
        public Guid? IDNhaCungCap { get; set; }
        public virtual NhaCungCap? NhaCungCap { get; set; }
        public virtual List<ChiTietPhieuNhap>? ChiTietPhieuNhaps { get; set; }
    }
}
