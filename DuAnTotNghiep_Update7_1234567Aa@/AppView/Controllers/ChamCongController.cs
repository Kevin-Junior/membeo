using AppData.Migrations;
using AppData.Models;
using AppData.ViewModels;
using AppView.PhanTrang;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ChamCongController : Controller
    {
        AssignmentDBContext _db;
        public int PageSize = 8;

        public ChamCongController()
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

            return View(new PhanTrangBase<ChamCong>
            {
                Items = _db.ChamCongs
                            .Where(x => x.IDNhanVien == loginInfor.Id)
                           .Include(x => x.NhanVien)
                           .Skip((ProductPage - 1) * PageSize).Take(PageSize)
                           .OrderByDescending(x => x.Time),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage = PageSize,
                    CurrentPage = ProductPage,
                    TotalItems = _db.ChamCongs
                    .Where(x => x.IDNhanVien == loginInfor.Id)
                    .Count()
                }
            });

        }

        [HttpGet]
        public IActionResult CheckIn()
        {
            var loginInfor = new LoginViewModel();
            string? session = HttpContext.Session.GetString("LoginInfor");
            
            if (string.IsNullOrEmpty(session)) {
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

            var items = _db.ChamCongs
                .Where(x => x.IDNhanVien == loginInfor.Id 
                && x.Time.Date == DateTime.Now.Date
                && x.Type == "CHECK_IN").ToList();
            if (items.Count > 0)
                return RedirectToAction("Show", "ChamCong");

            ChamCong model = new ChamCong();
            model.Id = Guid.NewGuid();
            model.IDNhanVien = loginInfor.Id;
            model.Time = DateTime.Now;
            model.Type = "CHECK_IN";

            _db.ChamCongs.Add(model);

            try
            {
                _db.SaveChanges();
                return RedirectToAction("Show", "ChamCong");
            } 
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult CheckOut()
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

            var items = _db.ChamCongs
               .Where(x => x.IDNhanVien == loginInfor.Id
               && x.Time.Date == DateTime.Now.Date
               && x.Type == "CHECK_OUT").ToList();
            if (items.Count > 0)
                return RedirectToAction("Show", "ChamCong");
            
            ChamCong model = new ChamCong();
            model.Id = Guid.NewGuid();
            model.IDNhanVien = loginInfor.Id;
            model.Time = DateTime.Now;
            model.Type = "CHECK_OUT";

            _db.ChamCongs.Add(model);

            try
            {
                _db.SaveChanges();
                return RedirectToAction("Show", "ChamCong");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
