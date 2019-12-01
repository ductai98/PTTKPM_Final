using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DTO
{
    class BanAnDTO
    {
        private int iDBanAn;
        private string tenBanAn;
        private string trangThaiBanAn;

        public BanAnDTO(int iDBanAn, string tenBanAn, string trangThaiBanAn)
        {
            IDBanAn = iDBanAn;
            TenBanAn = tenBanAn;
            TrangThaiBanAn = trangThaiBanAn;
        }

        public int IDBanAn { get => iDBanAn; set => iDBanAn = value; }
        public string TenBanAn { get => tenBanAn; set => tenBanAn = value; }
        public string TrangThaiBanAn { get => trangThaiBanAn; set => trangThaiBanAn = value; }
    }
}
