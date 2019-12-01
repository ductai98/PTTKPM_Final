using _1612580_QuanLyQuanTraSua.DAO;
using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.BUS
{
    class BanAnBUS
    {
        private static BanAnBUS _instance;
        public static BanAnBUS Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BanAnBUS();
                return _instance;
            }
            private set { _instance = value; }
        }

        public List<BanAnDTO> LoadDanhDanhBanAn()
        {
            return BanAnDAO.Instance.LoadDanhSachBanAn();
        }
    }
}
