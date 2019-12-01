using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class HoaDonDTO
    {
        private int idHoaDon;
        private int idTaiKhoan;
        private int idBanAn;
        private string loaiDonHang;
        private string ghichu;
        private DateTime ngayLapHoaDon;
        private DateTime? ngayThanhToan;
        private int trangThaiThanhToan;
        private int tongTien;

        public HoaDonDTO(int idHoaDon, int idTaiKhoan, int idBanAn, 
            string loaiDonHang, string ghichu, DateTime ngayLapHoaDon, 
            DateTime? ngayThanhToan, int trangThaiThanhToan, int tongTien)
        {
            IdHoaDon = idHoaDon;
            IdTaiKhoan = idTaiKhoan;
            IdBanAn = idBanAn;
            LoaiDonHang = loaiDonHang;
            Ghichu = ghichu;
            NgayLapHoaDon = ngayLapHoaDon;
            NgayThanhToan = ngayThanhToan;
            TrangThaiThanhToan = trangThaiThanhToan;
            TongTien = tongTien;
        }

        public int IdHoaDon { get => idHoaDon; set => idHoaDon = value; }
        public int IdTaiKhoan { get => idTaiKhoan; set => idTaiKhoan = value; }
        public int IdBanAn { get => idBanAn; set => idBanAn = value; }
        public string LoaiDonHang { get => loaiDonHang; set => loaiDonHang = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
        public DateTime NgayLapHoaDon { get => ngayLapHoaDon; set => ngayLapHoaDon = value; }
        public DateTime? NgayThanhToan { get => ngayThanhToan; set => ngayThanhToan = value; }
        public int TrangThaiThanhToan { get => trangThaiThanhToan; set => trangThaiThanhToan = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }
    }
}
