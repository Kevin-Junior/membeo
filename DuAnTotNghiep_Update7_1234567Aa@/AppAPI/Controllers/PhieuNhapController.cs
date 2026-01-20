using AppAPI.IServices;
using AppAPI.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuNhapController : ControllerBase
    {
        private readonly IPhieuNhapService _phieuNhapService;
        public PhieuNhapController()
        {
            _phieuNhapService = new PhieuNhapService();
        }

        [HttpGet("GetAll")]
        public List<PhieuNhap> GetAll()
        {
            return _phieuNhapService.GetAllPhieuNhap();
        }

        [HttpGet("GetById/{Id}")]
        public PhieuNhap GetById(Guid Id)
        {
            return _phieuNhapService.GetById(Id);
        }

        [HttpGet("TimKiemPhieuNhap/{Ma}")]
        public List<PhieuNhap> TimKiemPhieuNhap(string Ma)
        {
            return _phieuNhapService.GetAllPhieuNhap()
                .Where(x => x.MaPN.ToLower().Contains(Ma.ToLower()))
                .ToList();
        }

        [HttpPost("Create")]
        public PhieuNhap Create(PhieuNhap phieuNhap)
        {
            return _phieuNhapService.Create(phieuNhap);
        }

        [HttpPut("Update")]
        public bool Update(PhieuNhap phieuNhap)
        {
            return _phieuNhapService.Update(phieuNhap);
        }

        [HttpDelete("Delete/{Id}")]
        public bool Delete(Guid Id)
        {
            return _phieuNhapService.Delete(Id);
        }
    }
}
