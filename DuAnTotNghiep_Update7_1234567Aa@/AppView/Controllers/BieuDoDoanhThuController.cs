using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class BieuDoDoanhThuController : Controller
    {
        private readonly AssignmentDBContext dBContext;

        public BieuDoDoanhThuController()
        {
            dBContext = new AssignmentDBContext();
        }

        public IActionResult Index()
        {
            var year = DateTime.Now.Year;

            var items = dBContext.HoaDons
                .Where(x => x.NgayTao.Year == year && (x.TrangThaiGiaoHang == 2 || x.TrangThaiGiaoHang == 3 || x.TrangThaiGiaoHang == 6))
                .Join(dBContext.ChiTietHoaDons, x => x.ID, y => y.IDHoaDon,
                (x, y) => new
                {
                    T = x.NgayThanhToan.Value.Month,
                    M = y.SoLuong * y.DonGia
                })
                .GroupBy(x => x.T)
                .Select(x => new
                {
                    Thang = x.Key,
                    TongTien = x.Sum(c => c.M)
                })
                .ToList();

            List<int> revenues = new List<int>()
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

            decimal totalRevenue = 0;

            for (int i = 0; i < items.Count; i++)
            {
                revenues[items[i].Thang - 1] += items[i].TongTien;
                totalRevenue += items[i].TongTien;
            }

            ViewBag.Revenues = revenues;
            ViewBag.TotalRevenue = totalRevenue;
            return View();
        }
    }
}
