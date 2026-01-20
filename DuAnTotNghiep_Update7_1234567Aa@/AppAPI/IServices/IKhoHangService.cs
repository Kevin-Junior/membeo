using AppData.Models;

namespace AppAPI.IServices
{
    public interface IKhoHangService
    {
        KhoHang Create(KhoHang cap);
        bool Delete(Guid Id);
        List<KhoHang> GetAllKhoHang();
        KhoHang GetById(Guid Id);
        bool Update(KhoHang cap);
    }
}