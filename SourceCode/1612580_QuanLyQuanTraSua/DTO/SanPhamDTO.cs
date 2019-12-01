using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class SanPhamDTO
    {
        private int idSanPham;
        private int idDanhMuc;
        private int idKhuyenMai;
        private string tenSanPham;
        private int giaTien;

        public int IdSanPham { get => idSanPham; set => idSanPham = value; }
        public int IdDanhMuc { get => idDanhMuc; set => idDanhMuc = value; }
        public int IdKhuyenMai { get => idKhuyenMai; set => idKhuyenMai = value; }
        public string TenSanPham { get => tenSanPham; set => tenSanPham = value; }
        public int GiaTien { get => giaTien; set => giaTien = value; }

        public SanPhamDTO(int idSanPham, int idDanhMuc, int idKhuyenMai, string tenSanPham, int giaTien)
        {
            IdSanPham = idSanPham;
            IdDanhMuc = idDanhMuc;
            IdKhuyenMai = idKhuyenMai;
            TenSanPham = tenSanPham;
            GiaTien = giaTien;
        }
    }
}
