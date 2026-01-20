using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class SanPhamYeuThich
    {
        public SanPhamYeuThich()
        {
            Date = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid IDKhachHang { get; set; }
        public Guid IDSP { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(IDKhachHang))]
        public virtual KhachHang KhachHang { get; set; }

        [ForeignKey(nameof(IDSP))]
        public virtual SanPham SanPham { get; set; }
    }
}
