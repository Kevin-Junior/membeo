using AppAPI.IServices;
using AppData.Models;

namespace AppAPI.Services
{
    public class KhoHangService : IKhoHangService
    {
        private readonly AssignmentDBContext _dbContext;
        public KhoHangService()
        {
            _dbContext = new AssignmentDBContext();
        }

        public List<KhoHang> GetAllKhoHang()
        {
            return _dbContext.KhoHangs.ToList();
        }

        public KhoHang GetById(Guid Id)
        {
            return _dbContext.KhoHangs.Find(Id);
        }

        public KhoHang Create(KhoHang cap)
        {
            var obj = _dbContext.KhoHangs.Find(cap.ID);

            if (obj != null)
                return null;

            try
            {
                cap.ID = Guid.NewGuid();
                _dbContext.KhoHangs.Add(cap);
                _dbContext.SaveChanges();
                return cap;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới kho hàng !");
            }
        }

        public bool Update(KhoHang cap)
        {

            var obj = _dbContext.KhoHangs.Find(cap.ID);

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
                throw new Exception("Cập nhật thông tin kho hàng không thành công !");
            }
        }

        public bool Delete(Guid Id)
        {
            var obj = _dbContext.KhoHangs.Find(Id);

            if (obj == null)
                return false;

            try
            {
                _dbContext.KhoHangs.Remove(obj);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Xoá thông tin kho hàng không thành công !");
            }
        }
    }
}
