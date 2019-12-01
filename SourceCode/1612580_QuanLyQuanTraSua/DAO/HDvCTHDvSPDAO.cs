using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class HDvCTHDvSPDAO
    {
        private static HDvCTHDvSPDAO _instance;
        public static HDvCTHDvSPDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HDvCTHDvSPDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        //Hàm lấy tên món ăn, số lượng, đơn giá, tổng tiền để hiển thị lên list view và trình trạng thanh toán = 0 (chưa thanh toán)
        public List<HDvCTHDvSPDTO> HienThiListViewTheoBanAn(int idBanAn)
        {
            List<HDvCTHDvSPDTO> listViewitem = new List<HDvCTHDvSPDTO>();

            string query = $"select sp.tensanpham, cthd.soluong, sp.giatien, " +
                $"sp.giatien*cthd.soluong as tongTien from hoadon hd, chitiethoadon cthd, " +
                $"sanpham sp where hd.idhoadon = cthd.idhoadon and cthd.idsanpham = " +
                $"sp.idsanpham and hd.idbanan = '{idBanAn}' and hd.trangthaithanhtoan = 0";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                string tensp = row["tensanpham"].ToString();
                int soluong = (int)row["soluong"];
                int giatien = (int)row["giatien"];
                int tongtien = (int)row["tongtien"];
                HDvCTHDvSPDTO item = new HDvCTHDvSPDTO(tensp,soluong, giatien, tongtien);
                listViewitem.Add(item);
            }
            return listViewitem;
        }

        //Hàm trả ra tổng tiền theo bàn ăn
        public int HienThiTongTienTheoBan(int idBanAn)
        {
            string query = $"select SUM(sp.giatien*cthd.soluong) as thanhtien from hoadon hd, " +
                $"chitiethoadon cthd, sanpham sp where hd.idhoadon = cthd.idhoadon and cthd.idsanpham = " +
                $"sp.idsanpham and hd.idbanan = {idBanAn} and hd.trangthaithanhtoan = 0";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            int thanhTien = 0;
            if (dataTable.Rows[0]["thanhtien"].ToString() != "")
            {
                thanhTien = (int)dataTable.Rows[0]["thanhtien"];
            }
             
            return thanhTien;
        }

        //Hàm hiển thị hóa đơn mang về lên list view
        public List<HDvCTHDvSPDTO> HienThiHoaDonMangVe()
        {
            List<HDvCTHDvSPDTO> listViewitem = new List<HDvCTHDvSPDTO>();

            string query = $"select hd.idhoadon, sp.tensanpham, cthd.soluong, sp.giatien, sp.giatien*cthd.soluong as tongTien " +
                $"from hoadon hd, chitiethoadon cthd, sanpham sp " +
                $"where hd.idhoadon = cthd.idhoadon and cthd.idsanpham = sp.idsanpham and hd.idhoadon = " +
                $"(select MAX(hoadon.idhoadon) from hoadon)";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                string tensp = row["tensanpham"].ToString();
                int soluong = (int)row["soluong"];
                int giatien = (int)row["giatien"];
                int tongtien = (int)row["tongtien"];
                HDvCTHDvSPDTO item = new HDvCTHDvSPDTO(tensp, soluong, giatien, tongtien);
                listViewitem.Add(item);
            }
            return listViewitem;
        }

        //Hàm trả ra tổng tiền mang về
        public int HienThiTongTienMangVe()
        {
            string query = $"select top(1) SUM(sp.giatien*cthd.soluong) as thanhtien from hoadon hd, " +
                $"chitiethoadon cthd, sanpham sp where hd.idhoadon = cthd.idhoadon and cthd.idsanpham = " +
                $"sp.idsanpham group by hd.idhoadon order by hd.idhoadon DESC";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            int thanhTien = 0;
            if (dataTable.Rows[0]["thanhtien"].ToString() != "")
            {
                thanhTien = (int)dataTable.Rows[0]["thanhtien"];
            }

            return thanhTien;
        }
    }
}
