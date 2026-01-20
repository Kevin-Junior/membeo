using AppData.Models;

namespace AppAPI.IServices
{
    public interface IPhieuXuatService
    {
        PhieuXuat Create(PhieuXuat cap);
        bool Delete(Guid Id);
        List<PhieuXuat> GetAllPhieuXuat();
        PhieuXuat GetById(Guid Id);
        bool Update(PhieuXuat cap);
    }
}