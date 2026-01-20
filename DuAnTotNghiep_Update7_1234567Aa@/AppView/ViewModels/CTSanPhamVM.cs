namespace AppView.ViewModels
{
    public class CTSanPhamVM
    {
        public string MaSP { get; set; }
        public string HinhAnh { get; set; }
        public string TenSP { get; set; }
        public string MoTa { get; set; }
        public string LoaiSP { get; set; }
        public string MaCT { get; set; }
        public string MauSac { get; set; }
        public string KichCo { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public int DaBan { get; internal set; }
    }
}
