using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class DanhMucDAO
    {

        private static DanhMucDAO _instance;
        public static DanhMucDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DanhMucDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<DanhMucDTO> LoadDanhSachDanhMuc()
        {
            List<DanhMucDTO> listDanhMuc = new List<DanhMucDTO>();

            string query = "select * from danhmuc";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                int iddanhmuc = (int)row["iddanhmuc"];
                string tendanhmuc = row["tendanhmuc"].ToString();
                DanhMucDTO danhMuc = new DanhMucDTO(iddanhmuc,tendanhmuc);
                listDanhMuc.Add(danhMuc);
            }

            return listDanhMuc;
        }

        public void ThemDanhMuc(DanhMucDTO danhMuc)
        {
            string query = $"insert into danhmuc(tendanhmuc) values(N'{danhMuc.TenDanhMuc}')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void SuaDanhMuc(DanhMucDTO danhMuc)
        {
            string query = $"update danhmuc set tendanhmuc = N'{danhMuc.TenDanhMuc}' where iddanhmuc = {danhMuc.IDDanhMuc}";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void XoaDanhMuc(int id)
        {
            string query = $"update danhmuc set tendanhmuc = N'Deleted' where iddanhmuc = {id}";
            DataProvider.Instance.ExecuteNonQuery(query);
            string query2 = $"update sanpham set tensanpham = 'Deleted' " +
                            $"from sanpham sp join danhmuc dm on sp.iddanhmuc = dm.iddanhmuc " +
                            $"where dm.tendanhmuc = 'Deleted'";
            DataProvider.Instance.ExecuteNonQuery(query2);
        }
    }
}
