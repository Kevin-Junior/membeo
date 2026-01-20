using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.ViewModels.SanPham
{
    public class GioHangViewModel
    {
        public List<GioHangRequest> GioHangs { get; set; }
        public long TongTien { get; set; }
        public long GiamGia { 
            get
            {
                if (GioHangs.Count <= 1)
                    return 0;
                // Giảm 15% tổng số tiền
                var tongTien = (long) (GioHangs.Sum(x => x.SoLuong * x.DonGia) * 0.15);
                return tongTien;
            }
        }
    }
}
