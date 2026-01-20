using AppAPI.IServices;
using AppAPI.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoHangController : ControllerBase
    {
        private readonly IKhoHangService _khoHangService;
        public KhoHangController()
        {
            _khoHangService = new KhoHangService();
        }

        [HttpGet("GetAll")]
        public List<KhoHang> GetAll()
        {
            return _khoHangService.GetAllKhoHang();
        }

        [HttpGet("GetById/{Id}")]
        public KhoHang GetById(Guid Id)
        {
            return _khoHangService.GetById(Id);
        }

        [HttpGet("TimKiemKhoHang/{name}")]
        public List<KhoHang> TimKiemNhaCungCap(string name)
        {
            return _khoHangService.GetAllKhoHang()
                .Where(x => x.Ten.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        [HttpPost("Create")]
        public KhoHang Create(KhoHang KhoHang)
        {
            return _khoHangService.Create(KhoHang);
        }

        [HttpPut("Update")]
        public bool Update(KhoHang KhoHang)
        {
            return _khoHangService.Update(KhoHang);
        }

        [HttpDelete("Delete/{id}")]
        public bool Delete(Guid Id)
        {
            return _khoHangService.Delete(Id);
        }
    }
}
