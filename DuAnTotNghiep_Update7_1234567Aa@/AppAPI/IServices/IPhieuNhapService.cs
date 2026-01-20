using AppData.Models;

namespace AppAPI.IServices
{
    public interface IPhieuNhapService
    {
        PhieuNhap Create(PhieuNhap cap);
        bool Delete(Guid Id);
        List<PhieuNhap> GetAllPhieuNhap();
        PhieuNhap GetById(Guid Id);
        bool Update(PhieuNhap cap);
    }
}