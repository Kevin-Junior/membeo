using AppAPI.IServices;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace AppAPI.Services
{
    public class PhieuNhapService : IPhieuNhapService
    {
        private readonly AssignmentDBContext _dbContext;
        public PhieuNhapService()
        {
            _dbContext = new AssignmentDBContext();
        }

        public List<PhieuNhap> GetAllPhieuNhap()
        {
            return _dbContext
                .PhieuNhaps
                .Include(x => x.NhanVien)
                .Include(x => x.KhoHang)
                .Include(x => x.NhaCungCap)
                .ToList();
        }

        public PhieuNhap GetById(Guid Id)
        {
            return _dbContext
                .PhieuNhaps
                .Include(x => x.NhanVien)
                .Include(x => x.KhoHang)
                .Include(x => x.NhaCungCap)
                .Include(x => x.ChiTietPhieuNhaps)
                .Where(x => x.ID == Id)
                .FirstOrDefault();
        }

        public PhieuNhap Create(PhieuNhap cap)
        {
            if (cap.ChiTietPhieuNhaps == null)
                return null;

            var obj = _dbContext.PhieuNhaps.Find(cap.ID);

            if (obj != null)
                return null;

            try
            {
                cap.ID = Guid.NewGuid();
                
                _dbContext.PhieuNhaps.Add(cap);

                foreach (var chiTiet in cap.ChiTietPhieuNhaps)
                {
                    chiTiet.ID = Guid.NewGuid();
                    chiTiet.IDPhieuNhap = cap.ID;
                    _dbContext.ChiTietPhieuNhaps.Add(chiTiet);

                    var sp = _dbContext.ChiTietSanPhams.Find(chiTiet.IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong += chiTiet.SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.SaveChanges();

                
                return GetById(cap.ID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới phiếu nhập !");
            }
        }

        public bool Update(PhieuNhap cap)
        {
            var obj = _dbContext.PhieuNhaps.Find(cap.ID);

            if (obj == null)
                return false;

            try
            {
                _dbContext.Entry(obj).CurrentValues.SetValues(cap);
                
                var dsChiTietCu = _dbContext.ChiTietPhieuNhaps
                    .Where(x => x.IDPhieuNhap == cap.ID)
                    .ToList();

                for (int i = 0; i < dsChiTietCu.Count; i++)
                {
                    _dbContext.ChiTietPhieuNhaps.Remove(dsChiTietCu[i]);

                    var sp = _dbContext.ChiTietSanPhams.Find(dsChiTietCu[i].IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong -= dsChiTietCu[i].SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                foreach (var chiTiet in cap.ChiTietPhieuNhaps)
                {
                    chiTiet.ID = Guid.NewGuid();
                    chiTiet.IDPhieuNhap = cap.ID;
                    _dbContext.ChiTietPhieuNhaps.Add(chiTiet);

                    var sp = _dbContext.ChiTietSanPhams.Find(chiTiet.IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong += chiTiet.SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật thông tin phiếu nhập không thành công !");
            }
        }

        public bool Delete(Guid Id)
        {
            var obj = _dbContext.PhieuNhaps.Find(Id);

            if (obj == null)
                return false;

            try
            {
                var dsChiTietCu = _dbContext.ChiTietPhieuNhaps
                    .Where(x => x.IDPhieuNhap == obj.ID)
                    .ToList();

                for (int i = 0; i < dsChiTietCu.Count; i++)
                {
                    _dbContext.ChiTietPhieuNhaps.Remove(dsChiTietCu[i]);

                    var sp = _dbContext.ChiTietSanPhams.Find(dsChiTietCu[i].IDCTSP);

                    if (sp != null)
                    {
                        sp.SoLuong -= dsChiTietCu[i].SoLuong;
                        _dbContext.ChiTietSanPhams.Update(sp);
                    }
                }

                _dbContext.PhieuNhaps.Remove(obj);

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Xoá thông tin phiếu nhập không thành công !");
            }
        }
    }
}
