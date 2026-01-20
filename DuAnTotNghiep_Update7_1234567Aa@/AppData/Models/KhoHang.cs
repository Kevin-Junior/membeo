using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class KhoHang
    {
        public Guid ID { get; set; }
        [StringLength(200, ErrorMessage = "Tên kho không được để trống.")]
        public string Ten { get; set; }
        public string? DiaChi { get; set; }
        public IEnumerable<PhieuNhap>? PhieuNhaps { get; set; }
        public IEnumerable<PhieuXuat>? PhieuXuats { get; set; }
    }
}
