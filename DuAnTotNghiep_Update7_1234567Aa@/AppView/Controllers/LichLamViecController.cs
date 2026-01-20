using AppData.Models;
using AppData.ViewModels;
using AppView.PhanTrang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class LichLamViecController : Controller
    {
        AssignmentDBContext _db;
        public int PageSize = 8;

        public LichLamViecController()
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

            if (loginInfor.vaiTro.Value != 0) // Không phải nhân viên
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new PhanTrangBase<LichLamViec>
            {
                Items = _db.LichLamViecs
                           .Where(x => x.IdNhanVien == loginInfor.Id)
                           .Include(x => x.NhanVien)
                           .Skip((ProductPage - 1) * PageSize).Take(PageSize)
                           .OrderByDescending(x => x.Date),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _db.LichLamViecs
                    .Where(x => x.IdNhanVien == loginInfor.Id)
                    .Count()
                }
            });

        }

        [HttpGet]
        public IActionResult ShowAdmin(int ProductPage = 1)
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

            return View(new PhanTrangBase<LichLamViec>
            {
                Items = _db.LichLamViecs
                           .Include(x => x.NhanVien)
                           .Skip((ProductPage - 1) * PageSize).Take(PageSize)
                           .OrderByDescending(x => x.Date),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _db.LichLamViecs
                    .Count()
                }
            });

        }

        [HttpGet]
        public IActionResult Create()
        {
            var dsNhanVien = _db.NhanViens
                .Select(x => new SelectListItem()
                {
                    Value = x.ID.ToString(),
                    Text = x.Ten
                }).ToList();
            
            ViewBag.DSNhanVien = dsNhanVien;

            return View();
        }

        [HttpPost]
        public IActionResult Create(LichLamViec obj)
        {
            var dsNhanVien = _db.NhanViens
                .Select(x => new SelectListItem()
                {
                    Value = x.ID.ToString(),
                    Text = x.Ten
                }).ToList();

            ViewBag.DSNhanVien = dsNhanVien;

            obj.TrangThai = "CHUA_LAM";

            try
            {
                _db.LichLamViecs.Add(obj);
                _db.SaveChanges();
            } 
            catch (Exception ex)
            {
                return View(obj);
            }

            return RedirectToAction("ShowAdmin", "LichLamViec");
        }

        [HttpGet]
        public IActionResult Xoa(Guid id)
        {
            var lichLamViec = _db.LichLamViecs
                .Find(id);

            if (lichLamViec == null)
                return RedirectToAction("ShowAdmin", "LichLamViec");

            try
            {
                _db.LichLamViecs.Remove(lichLamViec);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("ShowAdmin", "LichLamViec");
            }

            return RedirectToAction("ShowAdmin", "LichLamViec");
        }
    }
}
