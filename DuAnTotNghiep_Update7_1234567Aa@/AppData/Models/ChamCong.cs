using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class ChamCong
    {
        public ChamCong()
        {
            Time = DateTime.Now;
        }
        public Guid Id { get; set; }
        public Guid IDNhanVien { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; } // CHECK IN / CHECK OUT
        [ForeignKey(nameof(IDNhanVien))]
        public virtual NhanVien NhanVien { get; set; }
    }
}
