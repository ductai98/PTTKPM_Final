using _1612580_QuanLyQuanTraSua.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.BUS
{
    class TaiKhoanBUS
    {
        TaiKhoanDAO taiKhoanDAO = new TaiKhoanDAO();
        public bool Login(string username, string password)
        {
            return taiKhoanDAO.Login(username,password);
        }
    }
}
