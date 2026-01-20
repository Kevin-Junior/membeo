using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class XepCa
    {
        public XepCa()
        {
             NgayHieuLuc = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string CaLamViec { get; set; }
        public Guid IdNhanVien { get; set; }
        public DateTime NgayHieuLuc { get; set; }
        [ForeignKey(nameof(IdNhanVien))]
        public virtual NhanVien NhanVien { get; set; }
    }
}
