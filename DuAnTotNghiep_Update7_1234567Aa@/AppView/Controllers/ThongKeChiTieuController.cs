using AppData.Models;
using AppData.ViewModels;
using AppView.Helpers;
using AppView.PhanTrang;
using AppView.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AppView.Controllers
{
    public class ThongKeChiTieuController : Controller
    {
        AssignmentDBContext _db;
        public int PageSize = 8;

        public ThongKeChiTieuController()
        {
            _db = new AssignmentDBContext();
        }

        [HttpGet]
        public IActionResult ThongKeTheoTuan(int? tuan = 1)
        {
            if (!tuan.HasValue)
                return RedirectToAction("Index", "Home");

            var loginInfor = new LoginViewModel();
            string? session = HttpContext.Session.GetString("LoginInfor");

            if (string.IsNullOrEmpty(session))
            {
                return RedirectToAction("Index", "Home");
            }

            loginInfor = JsonConvert.DeserializeObject<LoginViewModel>(session);

            if (loginInfor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!loginInfor.vaiTro.HasValue)
                return RedirectToAction("Index", "Home");

            if (loginInfor.vaiTro.Value != 1) // 1 - Khách Hàng
            {
                return RedirectToAction("Index", "Home");
            }

            var dsHDByKhachHang = _db.HoaDons
                .Where(x => x.IDKhachHang == loginInfor.Id)
                .Where(x => x.NgayTao.Year == DateTime.Now.Year)
                .OrderBy(x => x.NgayTao);

            List<int> weeks = new List<int>();
            // Lấy ra các tuần có phát sinh doanh thu
            foreach (var item in dsHDByKhachHang)
            {
                var w = DateTimeHelper.GetIso8601WeekOfYear(item.NgayTao);
                if (!weeks.Any(x => x == w))
                    weeks.Add(w);
            }

            ViewBag.Weeks = weeks;

            var firstDayOfWeek = DateTimeHelper
                .FirstDateOfWeekISO8601(DateTime.Now.Year, tuan.Value)
                .Date;

            var lastDayOfWeek = firstDayOfWeek.AddDays(7)
                .Date.AddHours(24);

            var dsHD = _db.HoaDons
                .Where(x => x.IDKhachHang == loginInfor.Id)
                .Where(x => x.NgayTao >= firstDayOfWeek && x.NgayTao <= lastDayOfWeek)
                .ToList();

            List<ThongKeChiTieuTuanVM> vm = new List<ThongKeChiTieuTuanVM>();

            var first = firstDayOfWeek;

            for (int i = 0; i < (lastDayOfWeek - firstDayOfWeek).Days; i++)
            {
                vm.Add(new ThongKeChiTieuTuanVM()
                {
                    Ngay = first.ToString("dd/MM/yyyy"),
                    Value = dsHD.Where(x => x.NgayTao.Year == first.Year
                        && x.NgayTao.Month == first.Month && x.NgayTao.Day == first.Day)
                    .Sum(x => x.TongTien).Value
                });

                first = first.AddDays(1);
            } 

            return View(vm);

        }

        [HttpGet]
        public IActionResult ThongKeTheoThang()
        {
            var loginInfor = new LoginViewModel();
            string? session = HttpContext.Session.GetString("LoginInfor");

            if (string.IsNullOrEmpty(session))
            {
                return RedirectToAction("Index", "Home");
            }

            loginInfor = JsonConvert.DeserializeObject<LoginViewModel>(session);

            if (loginInfor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!loginInfor.vaiTro.HasValue)
                return RedirectToAction("Index", "Home");

            if (loginInfor.vaiTro.Value != 1) // 1 - Khách Hàng
            {
                return RedirectToAction("Index", "Home");
            }

            var dsHD = _db.HoaDons
                .Where(x => x.IDKhachHang == loginInfor.Id)
                .Where(x => x.NgayTao.Year == DateTime.Now.Year)
                .ToList();

            List<ThongKeChiTieuThangVM> vm = new List<ThongKeChiTieuThangVM>();

            for (int i = 1; i <= 12; i++)
            {
                vm.Add(new ThongKeChiTieuThangVM()
                {
                    Thang = i.ToString(),
                    Value = dsHD.Where(x => x.NgayTao.Year == DateTime.Now.Year
                        && x.NgayTao.Month == i)
                    .Sum(x => x.TongTien).Value
                });
            }

            return View(vm);

        }

        [HttpGet]
        public IActionResult ThongKeTheoNam(int ProductPage = 1)
        {
            var loginInfor = new LoginViewModel();
            string? session = HttpContext.Session.GetString("LoginInfor");

            if (string.IsNullOrEmpty(session))
            {
                return RedirectToAction("Index", "Home");
            }

            loginInfor = JsonConvert.DeserializeObject<LoginViewModel>(session);

            if (loginInfor == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!loginInfor.vaiTro.HasValue)
                return RedirectToAction("Index", "Home");

            if (loginInfor.vaiTro.Value != 1) // 1 - Khách Hàng
            {
                return RedirectToAction("Index", "Home");
            }

            var dsHD = _db.HoaDons
                .Where(x => x.IDKhachHang == loginInfor.Id)
                .OrderBy(x => x.NgayTao)
                .ToList()
                .GroupBy(x => x.NgayTao.Year)
                .ToList();

            List<ThongKeChiTieuNamVM> vm = new List<ThongKeChiTieuNamVM>();

            foreach (var g in dsHD)
            {
                vm.Add(new ThongKeChiTieuNamVM()
                {
                    Nam = g.Key.ToString(),
                    Value = g
                    .Sum(x => x.TongTien).Value
                });
            }

            return View(vm);

        }
    }
}
