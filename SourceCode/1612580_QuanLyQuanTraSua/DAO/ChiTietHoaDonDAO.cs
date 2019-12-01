using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class ChiTietHoaDonDAO
    {
        private static ChiTietHoaDonDAO _instance;
        public static ChiTietHoaDonDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChiTietHoaDonDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<ChiTietHoaDonDTO> LayDanhSachCTHDTheoHoaDon(int idHoaDon)
        {
            List<ChiTietHoaDonDTO> listCTHD = new List<ChiTietHoaDonDTO>();
            string query = $"select * from chitiethoadon where idhoadon = '{idHoaDon}'";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in dataTable.Rows)
            {
                int idhoadon = (int)row["idhoadon"];
                int idsanpham = (int)row["idsanpham"];
                int soluong = (int)row["soluong"];
                double dongia = 0; //(double)row["dongia"];
                double tongtien = 0; //(double)row["tongtien"];

                ChiTietHoaDonDTO cthd = new ChiTietHoaDonDTO(idhoadon, idsanpham, soluong, dongia, tongtien);
                listCTHD.Add(cthd);
            }

            return listCTHD;
        }

        //Thêm chi tiết hóa đơn cho hóa đơn
        public int ThemCTHD(int idHoaDon, int idSanPham, int soLuong)
        {
            string query = "exec sp_themcthd @idhoadon, @idsanpham, @soluong";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { idHoaDon, idSanPham, soLuong });
        }
        
    }
}
