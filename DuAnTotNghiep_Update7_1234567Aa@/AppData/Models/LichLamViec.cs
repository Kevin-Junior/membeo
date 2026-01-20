using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class LichLamViec
    {
        public LichLamViec()
        {
            Date = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid IdNhanVien { get; set; }
        public DateTime Date { get; set; }
        public string CongViec { get; set; }
        public string TrangThai { get; set; } // Chưa làm
        [ForeignKey(nameof(IdNhanVien))]
        public virtual NhanVien NhanVien { get; set; }
    }
}
