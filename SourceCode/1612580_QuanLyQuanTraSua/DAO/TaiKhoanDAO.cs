using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class TaiKhoanDAO
    {
        private static TaiKhoanDAO _instance;
        public static TaiKhoanDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TaiKhoanDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        public bool Login(string username, string password)
        {
            string query = $"select * from taikhoan where tendangnhap = @username and matkhau = @password";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new string[] { username, password});
            if (result.Rows.Count <= 0)
            {
                return false;
            }
            return true;
        }

        public List<TaiKhoanDTO> LoadTaiKhoan()
        {
            string query = "select * from taikhoan";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            List<TaiKhoanDTO> listTaiKhoan = new List<TaiKhoanDTO>();
            foreach (DataRow row in dataTable.Rows)
            {
                int idtaikhoan = (int)row["idtaikhoan"];
                string tendangnhap = row["tendangnhap"].ToString();
                string matkhau = row["matkhau"].ToString();
                string email = row["email"].ToString();
                string hoten = row["hoten"].ToString();
                DateTime? ngaysinh = null;
                if (row["ngaysinh"].ToString() != "")
                {
                    ngaysinh = (DateTime?)row["ngaysinh"];
                }
                string gioitinh = row["gioitinh"].ToString();
                string chucvu = row["chucvu"].ToString();
                string sodienthoai = row["sodienthoai"].ToString();

                TaiKhoanDTO taiKhoan = new TaiKhoanDTO(idtaikhoan,tendangnhap,matkhau,email,hoten,ngaysinh,gioitinh,chucvu,sodienthoai);
                listTaiKhoan.Add(taiKhoan);
            }

            return listTaiKhoan;
        }

        public void ThemTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            string query = $"insert into taikhoan(tendangnhap, matkhau, email, hoten, " +
                $"ngaysinh, gioitinh, chucvu, sodienthoai) values(N'{taiKhoan.TenDangNhap}', " +
                $"N'{taiKhoan.MatKhau}', N'{taiKhoan.Email}', N'{taiKhoan.HoTen}', " +
                $"'{taiKhoan.NgaySinh}', N'{taiKhoan.GioiTinh}', N'{taiKhoan.ChucVu}', N'{taiKhoan.SoDienThoai}')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void SuaTaiKhoan(TaiKhoanDTO taiKhoan)
        {
            string query = $"update taikhoan set tendangnhap = N'{taiKhoan.TenDangNhap}', " +
                $"matkhau = N'{taiKhoan.MatKhau}', email = N'{taiKhoan.Email}', hoten = N'{taiKhoan.HoTen}', " +
                $"ngaysinh = '{taiKhoan.NgaySinh}', gioitinh = N'{taiKhoan.GioiTinh}', " +
                $"chucvu = N'{taiKhoan.ChucVu}', sodienthoai = N'{taiKhoan.SoDienThoai}' where idtaikhoan = {taiKhoan.IdTaiKhoan}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void XoaTaiKhoan(int idtaikhoan)
        {
            string query = $"update taikhoan set hoten = 'Deleted' where idtaikhoan = {idtaikhoan}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void DangKy(string username, string pass)
        {
            string query = $"insert into taikhoan(tendangnhap, matkhau, chucvu) values(N'{username}', N'{pass}', N'Staff')";
            TaiKhoanDTO taiKhoan = LayThongTinTaiKhoan(username);
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public TaiKhoanDTO LayThongTinTaiKhoan(string username)
        {
            string query = $"select * from taikhoan where tendangnhap = N'{username}'";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            int idtaikhoan = (int)dataTable.Rows[0]["idtaikhoan"];
            string tendangnhap = dataTable.Rows[0]["tendangnhap"].ToString();
            string matkhau = dataTable.Rows[0]["matkhau"].ToString();
            string email = dataTable.Rows[0]["email"].ToString();
            string hoten = dataTable.Rows[0]["hoten"].ToString();
            DateTime? ngaysinh = null;
            if (dataTable.Rows[0]["ngaysinh"].ToString() != "")
            {
                ngaysinh = (DateTime?)dataTable.Rows[0]["ngaysinh"];
            }
            string gioitinh = dataTable.Rows[0]["gioitinh"].ToString();
            string chucvu = dataTable.Rows[0]["chucvu"].ToString();
            string sodienthoai = dataTable.Rows[0]["sodienthoai"].ToString();

            TaiKhoanDTO taiKhoan = new TaiKhoanDTO(idtaikhoan, tendangnhap, matkhau, email, hoten, ngaysinh, gioitinh, chucvu, sodienthoai);
            return taiKhoan;
        }
    }
}
