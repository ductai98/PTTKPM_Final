using _1612580_QuanLyQuanTraSua.DAO;
using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.BUS
{
    class ChiTietHoaDonBUS
    {
        private static ChiTietHoaDonBUS _instance;
        public static ChiTietHoaDonBUS Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ChiTietHoaDonBUS();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<ChiTietHoaDonDTO> LayDanhSachCTHDtheoHoaDon(int idHoaDon)
        {
            return ChiTietHoaDonDAO.Instance.LayDanhSachCTHDTheoHoaDon(idHoaDon);
        }
    }
}
