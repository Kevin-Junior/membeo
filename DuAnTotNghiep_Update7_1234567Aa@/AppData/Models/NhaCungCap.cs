using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class NhaCungCap
    {
        public Guid ID { get; set; }
        [StringLength(200, ErrorMessage = "Tên nhà cung cấp không được dài quá 20 ký tự.")]
        public string Ten { get; set; }
        public string? Ma { get; set; }
        public string? DiaChi { get; set; }
        public IEnumerable<PhieuNhap>? PhieuNhaps { get; set; }
    }
}
