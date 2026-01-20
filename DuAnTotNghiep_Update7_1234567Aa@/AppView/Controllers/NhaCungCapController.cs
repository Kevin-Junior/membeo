using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Text;
using AppView.PhanTrang;
using DocumentFormat.OpenXml.Wordprocessing;
using AppView.Helpers;

namespace AppView.Controllers
{
    public class NhaCungCapController : Controller
    {
        private const string API_BASE_URL = "https://localhost";

        private readonly HttpClient _httpClient;

        public int PageSize = 8;
        public NhaCungCapController()
        {
            _httpClient = new HttpClient(new HttpClientLogHelper(new HttpClientHandler()));
            _httpClient.BaseAddress = new Uri("https://localhost:7095/api/");
        }

        public async Task<IActionResult> Show(int ProductPage = 1)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/NhaCungCap/GetAll";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var dsNhaCungCap = JsonConvert.DeserializeObject<List<NhaCungCap>>(apiData);
                return View(new PhanTrangBase<NhaCungCap>
                {
                    Items = dsNhaCungCap
                            .Skip((ProductPage - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PagingInfo
                    {
                        ItemsPerPage = PageSize,
                        CurrentPage = ProductPage,
                        TotalItems = dsNhaCungCap.Count()
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
                string apiUrl = $"https://localhost:7095/api/NhaCungCap/TimKiemNhaCungCap?name={Ten}";
                var response = await _httpClient.GetAsync(apiUrl);
                string apiData = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<NhaCungCap>>(apiData);
                if (users.Count == 0)
                {
                    ViewData["SearchError"] = "Không tìm thấy kết quả phù hợp";
                }
                return View("Show", new PhanTrangBase<NhaCungCap>
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
        public async Task<IActionResult> Create(NhaCungCap kc)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/NhaCungCap/Create";
                var json = JsonConvert.SerializeObject(kc);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PostAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Nhà cung cấp này đã có trong danh sách";
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
            string apiUrl = $"https://localhost:7095/api/NhaCungCap/GetById?id={id}";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<NhaCungCap>(apiData);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/NhaCungCap/GetById/{id}";
                var response = _httpClient.GetAsync(apiUrl).Result;
                var apiData = response.Content.ReadAsStringAsync().Result;
                var user = JsonConvert.DeserializeObject<NhaCungCap>(apiData);
                return View(user);
            }
            catch
            {
                return Redirect("https://localhost:5001/");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, NhaCungCap nv)
        {
            try
            {
                string apiUrl = $"https://localhost:7095/api/NhaCungCap/Update";
                var content = new StringContent(JsonConvert.SerializeObject(nv), Encoding.UTF8, "application/json");
                var reponsen = await _httpClient.PutAsync(apiUrl, content);
                if (reponsen.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else if (reponsen.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Nhà cung cấp này đã có trong danh sách";
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
            string apiUrl = $"https://localhost:7095/api/NhaCungCap/Delete/{id}";
            var reposen = await _httpClient.DeleteAsync(apiUrl);
            if (reposen.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            return RedirectToAction("Show");
        }
    }
}
