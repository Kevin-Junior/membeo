using AppAPI.IServices;
using AppAPI.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuXuatController : ControllerBase
    {
        private readonly IPhieuXuatService _phieuXuatService;
        public PhieuXuatController()
        {
            _phieuXuatService = new PhieuXuatService();
        }

        [HttpGet("GetAll")]
        public List<PhieuXuat> GetAll()
        {
            return _phieuXuatService.GetAllPhieuXuat();
        }

        [HttpGet("GetById/{Id}")]
        public PhieuXuat GetById(Guid Id)
        {
            return _phieuXuatService.GetById(Id);
        }

        [HttpGet("TimKiemPhieuXuat/{Ma}")]
        public List<PhieuXuat> TimKiemPhieuXuat(string Ma)
        {
            return _phieuXuatService.GetAllPhieuXuat()
                .Where(x => x.MaPX.ToLower().Contains(Ma.ToLower()))
                .ToList();
        }

        [HttpPost("Create")]
        public PhieuXuat Create(PhieuXuat PhieuXuat)
        {
            return _phieuXuatService.Create(PhieuXuat);
        }

        [HttpPut("Update")]
        public bool Update(PhieuXuat PhieuXuat)
        {
            return _phieuXuatService.Update(PhieuXuat);
        }

        [HttpDelete("Delete/{Id}")]
        public bool Delete(Guid Id)
        {
            return _phieuXuatService.Delete(Id);
        }
    }
}
