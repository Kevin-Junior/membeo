using AppData.Models;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class BaoCaoSanPhamController : Controller
    {
        private readonly AssignmentDBContext dBContext;

        public BaoCaoSanPhamController()
        {
            dBContext = new AssignmentDBContext();
        }

        public IActionResult Index()
        {
            var items = dBContext.ChiTietSanPhams
                .Include(x => x.SanPham)
                .ThenInclude(x => x.LoaiSP)
                .Include(x => x.SanPham)
                .ThenInclude(x => x.Anhs)
                .Include(x => x.ChiTietHoaDons)
                .Include(x => x.MauSac)
                .Include(x => x.KichCo)
            .Select(x => new CTSanPhamVM
            {
                MaSP = x.SanPham.Ma,
                HinhAnh = x.SanPham.Anhs.FirstOrDefault().DuongDan,
                TenSP = x.SanPham.Ten,
                MoTa = x.SanPham.MoTa,
                LoaiSP = x.SanPham.LoaiSP.Ten,
                MaCT = x.Ma,
                MauSac = x.MauSac.Ten,
                KichCo = x.KichCo.Ten,
                SoLuong = x.SoLuong,
                DonGia = x.GiaBan,
                DaBan = x.ChiTietHoaDons.Sum(x => x.SoLuong)
            })
            .ToList();

            return View(items);
        }
    }
}
