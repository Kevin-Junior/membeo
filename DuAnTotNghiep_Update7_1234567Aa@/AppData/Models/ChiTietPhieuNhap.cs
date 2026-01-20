using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class ChiTietPhieuNhap
    {
        public Guid ID { get; set; }
        [Required]
        [Range(1, 100000, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int SoLuong { get; set; }
        public Guid IDCTSP { get; set; }
        public Guid IDPhieuNhap { get; set; }
        public virtual PhieuNhap? PhieuNhap { get; set; }
        public virtual ChiTietSanPham? ChiTietSanPham { get; set; }
    }
}
