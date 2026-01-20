using AppData.Models;
using AppView.Helpers;
using AppView.PhanTrang;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AppView.Controllers
{
    public class KhoHangController : Controller
    {
        private const string API_BASE_URL = "https://localhost";

        private readonly HttpClient _httpClient;

        public int PageSize = 8;
        public KhoHangController()
        {
            _httpClient = new HttpClient(new HttpClientLogHelper(new HttpClientHandler()));
            _httpClient.BaseAddress = new Uri("https://localhost:7095/api/");
        }

        public async Task<IActionResult> Show(int ProductPage = 1)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/KhoHang/GetAll";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var dsKhoHang = JsonConvert.DeserializeObject<List<KhoHang>>(apiData);
                return View(new PhanTrangBase<KhoHang>
                {
                    Items = dsKhoHang
                            .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = dsKhoHang.Count()
                    }
                });
            }
            catch { return Redirect("https://localhost:5001/"); }
        }

        [HttpGet]
        public async Task<IActionResult> SearchTheoTen(string? Ten, int ProductPage = 1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Ten))
                {
                    ViewData["SearchError"] = "Vui lòng nhập tên để tìm kiếm";
                    return RedirectToAction("Show");
                }
                string apiUrl = $"https://localhost:7095/api/KhoHang/TimKiemKhoHang?name={Ten}";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<KhoHang>>(apiData);
                if (users.Count == 0)
                {
                    ViewData["SearchError"] = "Không tìm thấy kết quả phù hợp";
                }
                return View("Show", new PhanTrangBase<KhoHang>
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhoHang kc)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/KhoHang/Create";
                var json = JsonConvert.SerializeObject(kc);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PostAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Kho hàng này đã có trong danh sách";
                    return View();
                }
                return View(kc);
            }
            catch
            {
                return Redirect("https://localhost:5001/");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string apiUrl = $"https://localhost:7095/api/KhoHang/GetById?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<KhoHang>(apiData);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/KhoHang/GetById/{id}";
                var response = _httpClient.GetAsync(apiUrl).Result;
                var apiData = response.Content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<KhoHang>(apiData);
                return View(user);
            }
            catch
            {
                return Redirect("https://localhost:5001/");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, KhoHang nv)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/KhoHang/Update";
                var content = new StringContent(JsonConvert.SerializeObject(nv), Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PutAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Kho hàng này đã có trong danh sách";
                    return View();
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
            string apiUrl = $"https://localhost:7095/api/KhoHang/Delete/{id}";
            var reposen = await _httpClient.DeleteAsync(apiUrl);
            if (reposen.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
