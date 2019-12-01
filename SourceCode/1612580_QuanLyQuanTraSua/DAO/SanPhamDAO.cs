using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class SanPhamDAO
    {
        private static SanPhamDAO _instance;
        public static SanPhamDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SanPhamDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<SanPhamDTO> HienThiSanPhamTheoDanhMuc(int idDanhMuc)
        {
            List<SanPhamDTO> listSanPham = new List<SanPhamDTO>();
            string query = $"select idsanpham, iddanhmuc, idkhuyenmai, tensanpham, giatien from sanpham where iddanhmuc = {idDanhMuc}";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                int idsanpham = (int)row["idsanpham"];
                int iddanhmuc = (int)row["iddanhmuc"];
                int idkhuyenmai = 0;
                if (row["idkhuyenmai"].ToString() != "")
                {
                    idkhuyenmai = (int)row["idkhuyenmai"];
                }
                string tensanpham = row["tensanpham"].ToString();
                int giatien = (int)row["giatien"];
                SanPhamDTO sanPham = new SanPhamDTO(idsanpham, iddanhmuc, idkhuyenmai, tensanpham, giatien);
                listSanPham.Add(sanPham);
            }

            return listSanPham;
        }

        public void ThemSanPham(SanPhamDTO sanPham)
        {
            string query = $"insert into sanpham(iddanhmuc, tensanpham, giatien) values({sanPham.IdDanhMuc}, N'{sanPham.TenSanPham}', N'{sanPham.GiaTien}')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void SuaSanPham(SanPhamDTO sanPham)
        {
            string query = $"update sanpham set tensanpham = N'{sanPham.TenSanPham}', giatien = {sanPham.GiaTien} where idsanpham = {sanPham.IdSanPham}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void XoaSanPham(int id)
        {
            string query = $"update sanpham set tensanpham = N'Deleted' where idsanpham = {id}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
