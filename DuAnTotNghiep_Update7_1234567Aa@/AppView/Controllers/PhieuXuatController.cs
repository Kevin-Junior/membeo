using AppData.Models;
using AppView.Helpers;
using AppView.PhanTrang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AppView.Controllers
{
    public class PhieuXuatController : Controller
    {
        private const string API_BASE_URL = "https://localhost";

        private readonly HttpClient _httpClient;
        private readonly AssignmentDBContext _dbContext;
        public int PageSize = 8;
        public PhieuXuatController()
        {
            _httpClient = new HttpClient(new HttpClientLogHelper(new HttpClientHandler()));
            _httpClient.BaseAddress = new Uri("https://localhost:7095/api/");
            _dbContext = new AssignmentDBContext();
        }

        public async Task<IActionResult> Show(int ProductPage = 1)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/PhieuXuat/GetAll";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var dsPhieuXuat = JsonConvert.DeserializeObject<List<PhieuXuat>>(apiData);
                return View(new PhanTrangBase<PhieuXuat>
                {
                    Items = dsPhieuXuat
                            .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = dsPhieuXuat.Count()
                    }
                });
            }
            catch { return Redirect("https://localhost:5001/"); }
        }

        [HttpGet]
        public async Task<IActionResult> SearchTheoMa(string? Ma, int ProductPage = 1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Ma))
                {
                    ViewData["SearchError"] = "Vui lòng nhập tên để tìm kiếm";
                    return RedirectToAction("Show");
                }
                string apiUrl = $"https://localhost:7095/api/PhieuXuat/TimKiemPhieuXuat?Ma={Ma}";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<PhieuXuat>>(apiData) ?? new List<PhieuXuat>();
                if (users.Count == 0)
                {
                    ViewData["SearchError"] = "Không tìm thấy kết quả phù hợp";
                }

                return View("Show", new PhanTrangBase<PhieuXuat>
                {
                    Items = users
                             .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = users.Count()
                    }
                });
            }
            catch { return Redirect("https://localhost:5001/"); }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var dsNhanVien = _dbContext.NhanViens
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSNhanVien = new SelectList(dsNhanVien, "Value", "Text");
            var dsKhoHang = _dbContext.KhoHangs
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSKhoHang = new SelectList(dsKhoHang, "Value", "Text");
            var dsNhaCungCap = _dbContext.NhaCungCaps
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSNhaCungCap = new SelectList(dsNhaCungCap, "Value", "Text");

            PhieuXuat model = new PhieuXuat();
            model.ChiTietPhieuXuats = new List<ChiTietPhieuXuat>();
            model.ChiTietPhieuXuats.Add(new ChiTietPhieuXuat()
            {
                ID = Guid.NewGuid()
            });
            var dsSanPham = _dbContext
                .ChiTietSanPhams
                .Include(x => x.SanPham)
                .Include(x => x.MauSac)
                .Select(x => new
                {
                    ID = x.ID,
                    Ten = x.SanPham.Ten + " " + x.MauSac.Ten,
                    Gia = x.GiaBan,
                    MauSac = x.MauSac.Ten
                }).ToList();

            ViewBag.DSSanPham = new SelectList(dsSanPham, "ID", "Ten");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhieuXuat kc)
        {
            var dsNhanVien = _dbContext.NhanViens
            .Select(x => new SelectListItem()
            {
                Text = x.Ten,
                Value = x.ID.ToString()
            });
            ViewBag.DSNhanVien = new SelectList(dsNhanVien, "Value", "Text");
            var dsKhoHang = _dbContext.KhoHangs
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSKhoHang = new SelectList(dsKhoHang, "Value", "Text");
            var dsNhaCungCap = _dbContext.NhaCungCaps
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSNhaCungCap = new SelectList(dsNhaCungCap, "Value", "Text");

            var dsSanPham = _dbContext
                .ChiTietSanPhams
                .Include(x => x.SanPham)
                .Include(x => x.MauSac)
                .Select(x => new
                {
                    ID = x.ID,
                    Ten = x.SanPham.Ten + " " + x.MauSac.Ten,
                    Gia = x.GiaBan,
                    MauSac = x.MauSac.Ten
                }).ToList();

            ViewBag.DSSanPham = new SelectList(dsSanPham, "ID", "Ten");

            try
            {
                if (!kc.NgayTao.HasValue)
                    kc.NgayTao = DateTime.Now;

                string apiUrl = $"https://localhost:7095/api/PhieuXuat/Create";
                var json = JsonConvert.SerializeObject(kc);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PostAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Phiếu nhập này đã có trong danh sách";
                    return View(kc);
                }
                return View(kc);
            }
            catch
            {
                return Redirect("https://localhost:5001/");
            }
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                var dsNhanVien = _dbContext.NhanViens
               .Select(x => new SelectListItem()
               {
                   Text = x.Ten,
                   Value = x.ID.ToString()
               });
                ViewBag.DSNhanVien = new SelectList(dsNhanVien, "Value", "Text");
                var dsKhoHang = _dbContext.KhoHangs
                    .Select(x => new SelectListItem()
                    {
                        Text = x.Ten,
                        Value = x.ID.ToString()
                    });
                ViewBag.DSKhoHang = new SelectList(dsKhoHang, "Value", "Text");
                var dsNhaCungCap = _dbContext.NhaCungCaps
                    .Select(x => new SelectListItem()
                    {
                        Text = x.Ten,
                        Value = x.ID.ToString()
                    });
                ViewBag.DSNhaCungCap = new SelectList(dsNhaCungCap, "Value", "Text");

                string apiUrl = $"https://localhost:7095/api/PhieuXuat/GetById/{id}";
                var response = _httpClient.GetAsync(apiUrl).Result;
                var apiData = response.Content.ReadAsStringAsync().Result;
                var model = JsonConvert.DeserializeObject<PhieuXuat>(apiData);

                var dsSanPham = _dbContext
                    .ChiTietSanPhams
                    .Include(x => x.SanPham)
                    .Include(x => x.MauSac)
                    .Select(x => new
                    {
                        ID = x.ID,
                        Ten = x.SanPham.Ten + " " + x.MauSac.Ten,
                        Gia = x.GiaBan,
                        MauSac = x.MauSac.Ten
                    }).ToList();

                ViewBag.DSSanPham = new SelectList(dsSanPham, "ID", "Ten");
                return View(model);
            }
            catch (Exception ex)
            {
                return Redirect("https://localhost:5001/");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, PhieuXuat nv)
        {
            var dsNhanVien = _dbContext.NhanViens
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSNhanVien = new SelectList(dsNhanVien, "Value", "Text");
            var dsKhoHang = _dbContext.KhoHangs
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSKhoHang = new SelectList(dsKhoHang, "Value", "Text");
            var dsNhaCungCap = _dbContext.NhaCungCaps
                .Select(x => new SelectListItem()
                {
                    Text = x.Ten,
                    Value = x.ID.ToString()
                });
            ViewBag.DSNhaCungCap = new SelectList(dsNhaCungCap, "Value", "Text");

            var dsSanPham = _dbContext
                .ChiTietSanPhams
                .Include(x => x.SanPham)
                .Include(x => x.MauSac)
                .Select(x => new
                {
                    ID = x.ID,
                    Ten = x.SanPham.Ten + " " + x.MauSac.Ten,
                    Gia = x.GiaBan,
                    MauSac = x.MauSac.Ten
                }).ToList();

            ViewBag.DSSanPham = new SelectList(dsSanPham, "ID", "Ten");

            try
            {
                if (!nv.NgayTao.HasValue)
                    nv.NgayTao = DateTime.Now;
                string apiUrl = $"https://localhost:7095/api/PhieuXuat/Update";
                var content = new StringContent(JsonConvert.SerializeObject(nv), Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PutAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Phiếu nhập này đã có trong danh sách";
                    return View(nv);
                }
                return View(nv);
            }
            catch
            {
                return Redirect("https://localhost:5001/");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            string apiUrl = $"https://localhost:7095/api/PhieuXuat/Delete/{id}";
            var reposen = await _httpClient.DeleteAsync(apiUrl);
            if (reposen.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
