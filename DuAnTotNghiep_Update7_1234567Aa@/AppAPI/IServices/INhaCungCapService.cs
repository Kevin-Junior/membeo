using AppData.Models;

namespace AppAPI.IServices
{
    public interface INhaCungCapService
    {
        NhaCungCap Create(NhaCungCap cap);
        bool Delete(Guid Id);
        List<NhaCungCap> GetAllNhaCungCap();
        NhaCungCap GetById(Guid Id);
        bool Update(NhaCungCap cap);
    }
}