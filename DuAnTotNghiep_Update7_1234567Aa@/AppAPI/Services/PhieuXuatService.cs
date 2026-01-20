using AppAPI.IServices;
using AppData.Models;
using Microsoft.EntityFrameworkCore;

namespace AppAPI.Services
{
    public class PhieuXuatService : IPhieuXuatService
    {
        private readonly AssignmentDBContext _dbContext;
        public PhieuXuatService()
        {
            _dbContext = new AssignmentDBContext();
        }

        public List<PhieuXuat> GetAllPhieuXuat()
        {
            return _dbContext
                .PhieuXuats
                .Include(x => x.NhanVien)
                .Include(x => x.KhoHang)
                .ToList();
        }

        public PhieuXuat GetById(Guid Id)
        {
            return _dbContext
                .PhieuXuats
                .Include(x => x.NhanVien)
                .Include(x => x.KhoHang)
                .Include(x => x.ChiTietPhieuXuats)
                .Where(x => x.ID == Id)
                .FirstOrDefault();
        }

        public PhieuXuat Create(PhieuXuat cap)
        {
            if (cap.ChiTietPhieuXuats == null)
                return null;

            var obj = _dbContext.PhieuXuats.Find(cap.ID);

            if (obj != null)
                return null;

            try
            {
                cap.ID = Guid.NewGuid();

                _dbContext.PhieuXuats.Add(cap);

                foreach (var chiTiet in cap.ChiTietPhieuXuats)
                {
                    chiTiet.ID = Guid.NewGuid();
                    chiTiet.IDPhieuXuat = cap.ID;

                    _dbContext.ChiTietPhieuXuats.Add(chiTiet);

                    var sp = _dbContext.ChiTietSanPhams.Find(chiTiet.IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong -= chiTiet.SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.SaveChanges();


                return GetById(cap.ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới phiếu xuất !");
            }
        }

        public bool Update(PhieuXuat cap)
        {
            var obj = _dbContext.PhieuXuats.Find(cap.ID);

            if (obj == null)
                return false;

            try
            {
                _dbContext.Entry(obj).CurrentValues.SetValues(cap);

                var dsChiTietCu = _dbContext.ChiTietPhieuXuats
                    .AsNoTracking()
                    .Where(x => x.IDPhieuXuat == cap.ID)
                    .ToList();

                for (int i = 0; i < dsChiTietCu.Count; i++)
                {
                    _dbContext.ChiTietPhieuXuats.Remove(dsChiTietCu[i]);

                    var sp = _dbContext.ChiTietSanPhams.Find(dsChiTietCu[i].IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong += dsChiTietCu[i].SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                foreach (var chiTiet in cap.ChiTietPhieuXuats)
                {
                    chiTiet.ID = Guid.NewGuid();
                    chiTiet.IDPhieuXuat = cap.ID;
                    _dbContext.ChiTietPhieuXuats.Add(chiTiet);

                    var sp = _dbContext.ChiTietSanPhams.Find(chiTiet.IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong -= chiTiet.SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật thông tin phiếu xuất không thành công !");
            }
        }

        public bool Delete(Guid Id)
        {
            var obj = _dbContext.PhieuXuats.Find(Id);

            if (obj == null)
                return false;

            try
            {
                var dsChiTietCu = _dbContext.ChiTietPhieuXuats
                    .Where(x => x.IDPhieuXuat == obj.ID)
                    .ToList();

                for (int i = 0; i < dsChiTietCu.Count; i++)
                {
                    _dbContext.ChiTietPhieuXuats.Remove(dsChiTietCu[i]);

                    var sp = _dbContext.ChiTietSanPhams.Find(dsChiTietCu[i].IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong += dsChiTietCu[i].SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.PhieuXuats.Remove(obj);

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Xoá thông tin phiếu xuất không thành công !");
            }
        }
    }
}
