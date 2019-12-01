using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class ChiTietHoaDonDTO
    {
        private int idHoaDon;
        private int idSanPham;
        private int soLuong;
        private double donGia;
        private double tongTien;

        public ChiTietHoaDonDTO(int idHoaDon, int idSanPham, int soLuong, double donGia, double tongTien)
        {
            IdHoaDon = idHoaDon;
            IdSanPham = idSanPham;
            SoLuong = soLuong;
            DonGia = donGia;
            TongTien = tongTien;
        }

        public int IdHoaDon { get => idHoaDon; set => idHoaDon = value; }
        public int IdSanPham { get => idSanPham; set => idSanPham = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public double DonGia { get => donGia; set => donGia = value; }
        public double TongTien { get => tongTien; set => tongTien = value; }
    }
}
