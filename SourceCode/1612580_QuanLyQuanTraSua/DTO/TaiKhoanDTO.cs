using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    public class TaiKhoanDTO
    {
        private int idTaiKhoan;
        private string tenDangNhap;
        private string matKhau;
        private string email;
        private string hoTen;
        private DateTime? ngaySinh;
        private string gioiTinh;
        private string chucVu;
        private string soDienThoai;

        public int IdTaiKhoan { get => idTaiKhoan; set => idTaiKhoan = value; }
        public string TenDangNhap { get => tenDangNhap; set => tenDangNhap = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
        public string Email { get => email; set => email = value; }
        public string HoTen { get => hoTen; set => hoTen = value; }
        public DateTime? NgaySinh { get => ngaySinh; set => ngaySinh = value; }
        public string GioiTinh { get => gioiTinh; set => gioiTinh = value; }
        public string ChucVu { get => chucVu; set => chucVu = value; }
        public string SoDienThoai { get => soDienThoai; set => soDienThoai = value; }

        public TaiKhoanDTO(int idTaiKhoan, string tenDangNhap, string matKhau, string email, string hoTen, DateTime? ngaySinh, string gioiTinh, string chucVu, string soDienThoai)
        {
            IdTaiKhoan = idTaiKhoan;
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            Email = email;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            ChucVu = chucVu;
            SoDienThoai = soDienThoai;
        }
    }
}
