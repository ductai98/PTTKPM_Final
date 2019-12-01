using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class HDvCTHDvSPDTO
    {
        private string tenSanPham;
        private int soLuong;
        private int donGia;
        private int tongTien;

        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public int DonGia { get => donGia; set => donGia = value; }
        public int TongTien { get => tongTien; set => tongTien = value; }

        public HDvCTHDvSPDTO(string tenSanPham, int soLuong, int donGia, int tongTien)
        {
            TenSanPham = tenSanPham;
            SoLuong = soLuong;
            DonGia = donGia;
            TongTien = tongTien;
        }
    }
}
