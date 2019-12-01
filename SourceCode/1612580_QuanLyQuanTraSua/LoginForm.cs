using _1612580_QuanLyQuanTraSua.BUS;
using _1612580_QuanLyQuanTraSua.DAO;
using _1612580_QuanLyQuanTraSua.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1612580_QuanLyQuanTraSua
{
    public partial class LoginForm : Form
    {
        TaiKhoanBUS taiKhoanBUS = new TaiKhoanBUS();
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btn_Login(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (Login(username, password))
            {
                TaiKhoanDTO LoggedTaikhoan = TaiKhoanDAO.Instance.LayThongTinTaiKhoan(username);
                AdminForm adminForm = new AdminForm(LoggedTaikhoan);
                this.Hide();
                adminForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập");
            }
        }

        public bool Login(string user, string pass)
        {
            return taiKhoanBUS.Login(user, pass);
        }

        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if (txtDKPass.Text.Equals(txtDKRePass.Text))
            {
                string tendangnhap = txtDKUser.Text;
                string pass = txtDKPass.Text;
                TaiKhoanDTO tk = TaiKhoanDAO.Instance.LayThongTinTaiKhoan(tendangnhap);
                if (tk == null)
                {
                    TaiKhoanDAO.Instance.DangKy(tendangnhap, pass);
                }
                else
                {
                    MessageBox.Show("Đã tồn tại tên đăng nhập");
                }
            }
            else
            {
                MessageBox.Show("Nhập lại mật khẩu không khớp");
            }
        }
    }
}
