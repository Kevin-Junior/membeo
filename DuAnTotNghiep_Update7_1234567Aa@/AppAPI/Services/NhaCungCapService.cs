using AppAPI.IServices;
using AppData.Models;

namespace AppAPI.Services
{
    public class NhaCungCapService : INhaCungCapService
    {
        private readonly AssignmentDBContext _dbContext;
        public NhaCungCapService()
        {
            _dbContext = new AssignmentDBContext();
        }

        public List<NhaCungCap> GetAllNhaCungCap()
        {
            return _dbContext.NhaCungCaps.ToList();
        }

        public NhaCungCap GetById(Guid Id)
        {
            return _dbContext.NhaCungCaps.Find(Id);
        }

        public NhaCungCap Create(NhaCungCap cap) {
            var obj = _dbContext.NhaCungCaps.Find(cap.ID);

            if (obj != null)
                return null;
            
            try
            {
                cap.ID = Guid.NewGuid();
                _dbContext.NhaCungCaps.Add(cap);
                _dbContext.SaveChanges();
                return cap;
            }
            catch (Exception ex) {
                throw new Exception("Lỗi thêm mới nhà cung cấp !");
            }
        }

        public bool Update(NhaCungCap cap) {
            
            var obj = _dbContext.NhaCungCaps.Find(cap.ID);

            if (obj == null)
                return false;

            try
            {
                _dbContext.Entry(obj).CurrentValues.SetValues(cap);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật thông tin nhà cung cấp không thành công !");
            }
        }

        public bool Delete(Guid Id)
        {
            var obj = _dbContext.NhaCungCaps.Find(Id);

            if (obj == null)
                return false;

            try
            {
                _dbContext.NhaCungCaps.Remove(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                throw new Exception("Xoá thông tin nhà cung cấp không thành công !");
            }
        }
    }
}
