using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1612580_QuanLyQuanTraSua.DAO
{
    class BanAnDAO
    {
        private static BanAnDAO _instance;
        public static BanAnDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BanAnDAO();
                return _instance;
            }
            private set { _instance = value; }
        }
        public List<BanAnDTO> LoadDanhSachBanAn()
        {
            List<BanAnDTO> listBanAn = new List<BanAnDTO>();
            string query = "Select * from banan order by tenbanan ASC";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in dataTable.Rows)
            {
                int idbanan = (int)item["idbanan"];
                string tenbanan = (string)item["tenbanan"];
                string trangthaibanan = item["trangthaibanan"].ToString();

                BanAnDTO banAn = new BanAnDTO(idbanan, tenbanan, trangthaibanan);
                listBanAn.Add(banAn);
            }

            return listBanAn;
        }

        public int ThemBanAn(BanAnDTO banan)
        {
            int result = 0;
            string query = $"insert into banan(tenbanan, trangthaibanan) values(N'{banan.TenBanAn}', N'{banan.TrangThaiBanAn}')";
            result = DataProvider.Instance.ExecuteNonQuery(query);
            return result;
        }

        public int SuaBanAn(BanAnDTO banan)
        {
            int result = 0;
            string query = $"update banan set tenbanan = N'{banan.TenBanAn}' where idbanan = {banan.IDBanAn}";
            result = DataProvider.Instance.ExecuteNonQuery(query);
            return result;
        }

        public int XoaBanAn(int idbanan)
        {
            int result = 0;
            string query = $"update banan set tenbanan = N'Deleted' where idbanan = {idbanan}";
            result = DataProvider.Instance.ExecuteNonQuery(query);
            return result;
        }
    }
}
