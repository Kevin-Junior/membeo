using AppData.Models;
using AppData.ViewModels;
using AppView.PhanTrang;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class SanPhamYeuThichController : Controller
    {
        AssignmentDBContext _db;
        public int PageSize = 8;

        public SanPhamYeuThichController()
        {
            _db = new AssignmentDBContext();
        }

        [HttpGet]
        public IActionResult Show(int ProductPage = 1)
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

            return View(new PhanTrangBase<SanPhamYeuThich>
            {
                Items = _db.SanPhamYeuThichs
                           .Where(x => x.IDKhachHang == loginInfor.Id)
                           .Include(x => x.KhachHang)
                           .Include(x => x.SanPham)
                           .Skip((ProductPage - 1) * PageSize).Take(PageSize)
                           .OrderByDescending(x => x.Date),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _db.SanPhamYeuThichs
                    .Where(x => x.IDKhachHang == loginInfor.Id)
                    .Count()
                }
            });

        }

        [HttpGet]
        public IActionResult Create(Guid Id)
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

            var sp = _db.SanPhams
                .Where(x => x.ID == Id)
                .FirstOrDefault();
            
            if (sp == null) 
                return RedirectToAction("Index", "Home");

            SanPhamYeuThich obj = new SanPhamYeuThich();
            obj.IDKhachHang = loginInfor.Id;
            obj.IDSP = sp.ID;
            obj.Date = DateTime.Now;

            try
            {
                _db.SanPhamYeuThichs.Add(obj);
                _db.SaveChanges();
            } 
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Show", "SanPhamYeuThich");

        }

        [HttpGet]
        public IActionResult Delete(Guid Id)
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

            var sp = _db.SanPhamYeuThichs
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            if (sp == null)
                return RedirectToAction("Index", "Home");

            try
            {
                _db.SanPhamYeuThichs.Remove(sp);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Show", "SanPhamYeuThich");

        }
    }
}
