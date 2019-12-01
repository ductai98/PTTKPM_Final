using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class HoaDonDAO
    {
        private static HoaDonDAO _instance;
        public static HoaDonDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HoaDonDAO();
                return _instance;
            }
            private set { _instance = value; }
        }

        //Lấy id của những hóa đơn theo id bàn ăn (hóa đơn chưa được thanh toán) trangthaithanhtoan = 0 là chưa thanh toán
        //Thành công trả về id hóa đơn, thất bại trả về 1
        public int LayHoaDonTheoBanAn(int idBanAn)
        {
            string query = $"select * from hoadon where idbanan = '{idBanAn}' and trangthaithanhtoan = 0";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            if (dataTable.Rows.Count > 0)
            {
                int idHoaDon = (int)dataTable.Rows[0]["idhoadon"];
                int idTaiKhoan = 0; //= (int)dataTable.Rows[0]["idtaikhoan"];
                int idBanAn1 = (int)dataTable.Rows[0]["idbanan"];
                string loaidonhang = dataTable.Rows[0]["loaidonhang"].ToString();
                string ghichu = dataTable.Rows[0]["ghichu"].ToString();
                int trangthai = (int)dataTable.Rows[0]["trangthaithanhtoan"];
                int thanhTien = 0;//(double)dataTable.Rows[0]["giatien"];
                DateTime ngaylap = (DateTime)dataTable.Rows[0]["ngaylaphoadon"];
                DateTime? ngaythanhtoan = null;
                if (dataTable.Rows[0]["ngaythanhtoan"].ToString() != "")
                {
                    ngaythanhtoan = (DateTime?)dataTable.Rows[0]["ngaythanhtoan"];
                }
                HoaDonDTO hoaDon = new HoaDonDTO(idHoaDon, idTaiKhoan, idBanAn1, loaidonhang, ghichu, ngaylap,
                                                    ngaythanhtoan, trangthai, thanhTien);

                return hoaDon.IdHoaDon;
            }
            return -1;
        }


        //Hàm thêm hóa đơn cho bàn ăn vào csdl
        public bool ThemHoaDonChoBanAn(int idBanAn)
        {
            string query = "exec sp_themhoadonchobanan @idbanan";
            int kq = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBanAn });
            if (kq <= 0)
            {
                return false;
            }
            return true;
        }

        //Hàm lấy id hóa đơn lớn nhất để thêm vào chi tiết hóa đơn
        public int LayIdHoaDonLonNhat()
        {
            int max = 0;
            string query = "select MAX(idhoadon) as max from hoadon";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            max = (int)data.Rows[0]["max"];
            return max;
        }

        //Hàm thêm hóa đơn mang về
        public void ThemHoaDonMangVe()
        {
            string query = $"insert into hoadon(ngaylaphoadon, ngaythanhtoan, trangthaithanhtoan, loaidonhang) " +
                $"values('{DateTime.Now}', null, 0, N'Mang về')";
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        //Hàm thanh toán hóa đơn cho bàn ăn
        public void ThanhToanHoaDon(int idhoadon, int tongTien, int? idbanan)
        {
            DataProvider.Instance.ExecuteNonQuery("exec sp_thanhtoanhoadon @idhoadon, @tongTien, @idbanan", new object[] { idhoadon, tongTien, idbanan });
        }

        //Hàm thanh toán hóa đơn mang về
        public void ThanhToanHoaDonMangVe(int idhoadon, int tongTien)
        {
            DataProvider.Instance.ExecuteNonQuery("exec sp_thanhtoanhoadon @idhoadon, @tongTien", new object[] { idhoadon, tongTien });
        }

        //Hàm hiển thị danh sach hóa đơn trong tab hóa dơn
        public List<HoaDonDTO> HienThiDanhSachHoaDon()
        {
            List<HoaDonDTO> listHoadon = new List<HoaDonDTO>();
            string query = "select * from hoadon";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dataTable.Rows)
            {
                int idhoadon = (int)item["idhoadon"];
                int idtaikhoan = 0;
                if (item["idtaikhoan"].ToString() != "")
                {
                    idtaikhoan = (int)item["idtaikhoan"];
                }
                int idbanan = 0;
                if (item["idbanan"].ToString() != "")
                {
                    idbanan = (int)item["idbanan"];
                }
                string loaidonhang = item["loaidonhang"].ToString();
                DateTime ngaylaphoadon = (DateTime)item["ngaylaphoadon"];
                DateTime? ngaythanhtoan = null;
                if (item["ngaythanhtoan"].ToString() != "")
                {
                    ngaythanhtoan = (DateTime?)item["ngaythanhtoan"];
                }
                int trangthaithanhtoan = (int)item["trangthaithanhtoan"];
                int thanhTien = 0;
                if (item["giatien"].ToString() != "")
                {
                    thanhTien = (int)item["giatien"];
                }

                HoaDonDTO hoadon = new HoaDonDTO(idhoadon, idtaikhoan, idbanan, loaidonhang, null, ngaylaphoadon, ngaythanhtoan, trangthaithanhtoan, thanhTien);
                listHoadon.Add(hoadon);
            }
            return listHoadon;
        }

        //Thống kê doanh số theo tháng
        public DataTable ThongKeTheoThang(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_layhoadontheothang @date", new object[] { date });
        }

        //Thống kê doanh số theo ngày
        public DataTable ThongKeTheoNgay(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_layhoadontheongay @date", new object[] { date });
        }

        //Thống kê doanh số cụ thể
        public DataTable ThongKeCuThe(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_layhoadoncuthe @date", new object[] { date });
        }

        //Thống kê sản phẩm theo ngày
        public DataTable ThongKeSanPhamTheoNgay(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_thongkesanphamtheongay @date", new object[] { date });
        }

        //Thống kê sản phẩm theo tháng
        public DataTable ThongKeSanPhamTheoThang(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_thongkesanphamtheothang @date", new object[] { date });
        }

        //Thống kê sản phẩm tự chọn
        public DataTable ThongKeSanPhamTuChon(DateTime date)
        {
            return DataProvider.Instance.ExecuteQuery("exec sp_thongkesanphamtuchon @date", new object[] { date });
        }

    }
}
