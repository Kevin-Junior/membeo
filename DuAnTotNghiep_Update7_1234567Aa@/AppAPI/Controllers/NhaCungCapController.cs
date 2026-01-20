using AppAPI.IServices;
using AppAPI.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaCungCapController : ControllerBase
    {
        private readonly INhaCungCapService _nhaCungCapService;
        public NhaCungCapController()
        {
            _nhaCungCapService = new NhaCungCapService();
        }

        [HttpGet("GetAll")]
        public List<NhaCungCap> GetAll()
        {
            return _nhaCungCapService.GetAllNhaCungCap();
        }

        [HttpGet("TimKiemNhaCungCap/{name}")]
        public List<NhaCungCap> TimKiemNhaCungCap(string name)
        {
            return _nhaCungCapService.GetAllNhaCungCap()
                .Where(x => x.Ten.ToLower().Contains(name.ToLower()))
                .ToList();
        }

        [HttpGet("GetById/{Id}")]
        public NhaCungCap GetById(Guid Id)
        {
            return _nhaCungCapService.GetById(Id);
        }

        [HttpPost("Create")]
        public NhaCungCap Create([FromBody] NhaCungCap NhaCungCap)
        {
            return _nhaCungCapService.Create(NhaCungCap);
        }

        [HttpPut("Update")]
        public bool Update(NhaCungCap NhaCungCap)
        {
            return _nhaCungCapService.Update(NhaCungCap);
        }

        [HttpDelete("Delete/{id}")]
        public bool Delete(Guid Id)
        {
            return _nhaCungCapService.Delete(Id);
        }
    }
}
