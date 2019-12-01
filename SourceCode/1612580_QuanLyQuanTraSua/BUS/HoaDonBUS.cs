using _1612580_QuanLyQuanTraSua.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.BUS
{
    class HoaDonBUS
    {
        private static HoaDonBUS _instance;
        public static HoaDonBUS Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HoaDonBUS();
                return _instance;
            }
            private set { _instance = value; }
        }

        public int LayIDHoaDonTheoBanAn(int idBanAn)
        {
            return HoaDonDAO.Instance.LayHoaDonTheoBanAn(idBanAn);
        }
    }
}
