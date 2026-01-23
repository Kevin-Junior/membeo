using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppData.Models
{
    public class AssignmentDBContext : DbContext
    {
        public AssignmentDBContext()
        {
        }
        public AssignmentDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPhams { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<KichCo> KichCos { get; set; }
        public DbSet<ChatLieu> ChatLieus { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<LichSuTichDiem> LichSuTichDiems { get; set; }
        public DbSet<LoaiSP> LoaiSPs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<QuyDoiDiem> QuyDoiDiems { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Anh> Anhs { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<PhieuXuat> PhieuXuats { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<LichLamViec> LichLamViecs { get; set; }
        public DbSet<SanPhamYeuThich> SanPhamYeuThichs { get; set; }
        public DbSet<XepCa> XepCas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=thoitrang2;Integrated Security=True");
        }
        //THUYNHU\SQLEXPRESS
        //DESKTOP-UOIH77U\SQLEXPRESS
        //LAPTOP-A15NGLBG\SQLEXPRESS
        // lam DESKTOP-S6G7NFV\SQLEXPRESS // 1AppBanQuanAoThoiTrangNam
        //LAPTOP-G189FU38\SQLEXPRESS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
