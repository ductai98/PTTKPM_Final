using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class DanhMucDTO
    {
        private int _IDDanhMuc;
        public int IDDanhMuc
        {
            get { return _IDDanhMuc; }
            set { _IDDanhMuc = value; }
        }

        private string _TenDanhMuc;
        public string TenDanhMuc
        {
            get { return _TenDanhMuc; }
            set { _TenDanhMuc = value; }
        }

        public DanhMucDTO(int iDDanhMuc, string tenDanhMuc)
        {
            IDDanhMuc = iDDanhMuc;
            TenDanhMuc = tenDanhMuc;
        }
    }
}
