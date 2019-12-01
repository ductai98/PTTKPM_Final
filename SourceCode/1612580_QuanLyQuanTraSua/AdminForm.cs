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
    public partial class AdminForm : Form
    {
        private TaiKhoanDTO loggedTK;

        public TaiKhoanDTO LoggedTK
        {
            get => loggedTK;
            set
            {
                loggedTK = value;
                VerifyTK(loggedTK.ChucVu);
            }
        }

        public AdminForm(TaiKhoanDTO taiKhoan )
        {
            InitializeComponent();
            this.LoggedTK = taiKhoan;
            LoadDanhSachBanAn();
            HienThiDanhSachDanhMuc();
            HienThiDanhSachHoaDon(HoaDonDAO.Instance.HienThiDanhSachHoaDon());
            LoadDanhMucTabThucAn();
            LoadTaiKhoan();
        }

        //Hàm verify tài khoản
        void VerifyTK(string chucvu)
        {
            if (chucvu.Equals("Staff"))
            {
                ((Control)this.tabKhuyenmai).Enabled = false;
                ((Control)this.tabTaikhoan).Enabled = false;
                ((Control)this.tabMenu).Enabled = false;
                ((Control)this.tabBanan).Enabled = false;
            }
        }

        //Load danh sách bàn ăn từ CSDL
        void LoadDanhSachBanAn()
        {
            List<BanAnDTO> listBanAn = BanAnBUS.Instance.LoadDanhDanhBanAn();
            var bindingList = new BindingList<BanAnDTO>(listBanAn);
            var source = new BindingSource(bindingList, null);
            dtgrBanan2.DataSource = source;
            dtgrBanan2.Columns["IDBanAn"].Visible = false;
            dtgrBanan2.Columns["TenBanAn"].HeaderText = "Tên bàn ăn";
            dtgrBanan2.Columns["TrangThaiBanAn"].HeaderText = "Trạng thái";

            dgvBanan1.DataSource = source;
            dgvBanan1.Columns["IDBanAn"].Visible = false;
            dgvBanan1.Columns["TenBanAn"].HeaderText = "Tên bàn ăn";
            dgvBanan1.Columns["TrangThaiBanAn"].HeaderText = "Trạng thái";
        }

        //Show hóa đơn cho từng bàn
        void ShowHoaDonChoBanAn(int idBanAn)
        {
            lvHoaDon1.Items.Clear();
            List<HDvCTHDvSPDTO> listCTHD = HDvCTHDvSPDAO.Instance.HienThiListViewTheoBanAn(idBanAn);
            foreach (var item in listCTHD)
            {
                ListViewItem lvItem = new ListViewItem(item.TenSanPham);
                lvItem.SubItems.Add(item.SoLuong.ToString());
                lvItem.SubItems.Add(item.DonGia.ToString());
                lvItem.SubItems.Add(item.TongTien.ToString());
                lvHoaDon1.Items.Add(lvItem);
            }
        }

        void ShowHoaDonMangVe()
        {
            lvHoaDon1.Items.Clear();
            List<HDvCTHDvSPDTO> listCTHD = HDvCTHDvSPDAO.Instance.HienThiHoaDonMangVe();
            foreach (var item in listCTHD)
            {
                ListViewItem lvItem = new ListViewItem(item.TenSanPham);
                lvItem.SubItems.Add(item.SoLuong.ToString());
                lvItem.SubItems.Add(item.DonGia.ToString());
                lvItem.SubItems.Add(item.TongTien.ToString());
                lvHoaDon1.Items.Add(lvItem);
            }
        }

        //Show thành tiền cho mỗi bàn ăn khi click
        void ShowThanhTienChoBanAn(int idBanAn)
        {
            lblThanhTien.Text = HDvCTHDvSPDAO.Instance.HienThiTongTienTheoBan(idBanAn).ToString();
        }

        //Show thành tiền mang về
        void ShowThanhTienMangVe()
        {
            lblThanhTien.Text = HDvCTHDvSPDAO.Instance.HienThiTongTienMangVe().ToString();
        }

        //Hiển thị danh sách danh mục cho người dùng chọn
        void HienThiDanhSachDanhMuc()
        {
            List<DanhMucDTO> listDanhMuc = DanhMucDAO.Instance.LoadDanhSachDanhMuc();
            listDanhMuc.RemoveAll(item => item.TenDanhMuc.Equals("Deleted"));
            cmbChonDM.DataSource = listDanhMuc;
            cmbChonDM.DisplayMember = "TenDanhMuc";
        }


        //Hiển thị danh mục cho Tab thức ăn
        void LoadDanhMucTabThucAn()
        {
            List<DanhMucDTO> listDanhMuc = DanhMucDAO.Instance.LoadDanhSachDanhMuc();
            dtgrDanhMuc.DataSource = listDanhMuc;
            dtgrDanhMuc.Columns["IdDanhMuc"].Visible = false;
            dtgrDanhMuc.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
        }

        //Hiển thị sản phẩm theo danh mục
        void HienThiSanPhamTheoDanhMuc(int idDanhMuc)
        {
            List<SanPhamDTO> listSanPham = SanPhamDAO.Instance.HienThiSanPhamTheoDanhMuc(idDanhMuc);
            dataGridThucdon.DataSource = listSanPham;
            dataGridThucdon.Columns["IdSanPham"].Visible = false;
            dataGridThucdon.Columns["IdKhuyenMai"].Visible = false;
            dataGridThucdon.Columns["IdDanhMuc"].Visible = false;
            dataGridThucdon.Columns["TenSanPham"].HeaderText = "Tên món";
            dataGridThucdon.Columns["GiaTien"].HeaderText = "Đơn giá";

        }
        void HienThiSanPhamTheoDanhMucTabThucAn(int idDanhMuc)
        {
            List<SanPhamDTO> listSanPham = SanPhamDAO.Instance.HienThiSanPhamTheoDanhMuc(idDanhMuc);
            dtgrMonAn.DataSource = listSanPham;
            dtgrMonAn.Columns["IdSanPham"].Visible = false;
            dtgrMonAn.Columns["IdKhuyenMai"].Visible = false;
            dtgrMonAn.Columns["IdDanhMuc"].Visible = false;
            dtgrMonAn.Columns["TenSanPham"].HeaderText = "Tên món";
            dtgrMonAn.Columns["GiaTien"].HeaderText = "Đơn giá";

        }

        //Show danh sách hóa đơn trong tab hóa đơn
        void HienThiDanhSachHoaDon(List<HoaDonDTO> listHoadon)
        {
            dataGridHoaDon.DataSource = listHoadon;
            dataGridHoaDon.Columns["IdBanAn"].Visible = false;
            dataGridHoaDon.Columns["IdHoaDon"].Visible = false;
            dataGridHoaDon.Columns["IdTaiKhoan"].Visible = false;
            dataGridHoaDon.Columns["Ghichu"].Visible = false;
            dataGridHoaDon.Columns["LoaiDonHang"].HeaderText = "Loại đơn hàng";
            dataGridHoaDon.Columns["NgayLapHoaDon"].HeaderText = "Ngày lập";
            dataGridHoaDon.Columns["NgayThanhToan"].HeaderText = "Ngày thanh toán";
            dataGridHoaDon.Columns["TrangThaiThanhToan"].HeaderText = "Trạng thái";
            dataGridHoaDon.Columns["TongTien"].HeaderText = "Thành tiền";
        }

        //Load danh sách tài khoản
        void LoadTaiKhoan()
        {
            List<TaiKhoanDTO> listTaiKhoan = TaiKhoanDAO.Instance.LoadTaiKhoan();
            dataGridTaiKhoan.DataSource = listTaiKhoan;
            dataGridTaiKhoan.Columns["IdTaiKhoan"].Visible = false;
        }

        //Thống kê theo tháng
        void ThongKeTheoThang(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeTheoThang(date);
        }

        //Thống kê theo ngày
        void ThongKeTheoNgay(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeTheoNgay(date);
        }

        //Thống kê tự chọn
        void ThongKeTuChon(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeCuThe(date);
        }

        //Thống kê sản phẩm theo ngày
        void ThongKeSanPhamTheoNgay(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeSanPhamTheoNgay(date);
        }

        //Thống kê sản phẩm theo tháng
        void ThongKeSanPhamTheoThang(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeSanPhamTheoThang(date);
        }

        //Thống kê sản phẩm tự chọn
        void ThongKeSanPhamTuChon(DateTime date)
        {
            dataGridThongKe.DataSource = HoaDonDAO.Instance.ThongKeSanPhamTuChon(date);
        }



        //Vùng sự kiện
        //Các bàn có người sẽ được tô màu vàng, bàn đã xóa sẽ không hiển thị
        private void dgvBanAn2_RowPaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtgrBanan2.Rows[e.RowIndex].Cells[2].Value.ToString().Equals("Có người"))
            {
                dtgrBanan2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }

            if (dtgrBanan2.Rows[e.RowIndex].Cells[1].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dtgrBanan2.DataSource];
                currencyManager.SuspendBinding();
                dtgrBanan2.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }
        //Các bàn có người sẽ được tô màu vàng
        private void BanAn1_Paint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dgvBanan1.Rows[e.RowIndex].Cells[2].Value.ToString().Equals("Có người"))
            {
                dgvBanan1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
            }
            if (dgvBanan1.Rows[e.RowIndex].Cells[1].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dgvBanan1.DataSource];
                currencyManager.SuspendBinding();
                dgvBanan1.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }

        //Sử kiện show hóa đơn cho từng bàn
        private void dataGridHoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Sự kiện chọn danh mục trong comboBox
        private void cmbChonDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idDanhMuc = (cmbChonDM.SelectedItem as DanhMucDTO).IDDanhMuc;
            HienThiSanPhamTheoDanhMuc(idDanhMuc);
        }

        //Sự kiện click thêm món ăn cho hóa đơn
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (radioAnTaiBan.Checked)
            {
                if (dgvBanan1.CurrentRow.Selected)
                {
                    int idbanan = (int)dgvBanan1.CurrentRow.Cells[0].Value;
                    int idhoadon = HoaDonDAO.Instance.LayHoaDonTheoBanAn(idbanan);

                    if (idhoadon == -1) //Nếu bàn ăn chưa tồn tại hóa đơn thì thêm mới hóa đơn và chi tiết hóa đơn
                    {
                        HoaDonDAO.Instance.ThemHoaDonChoBanAn(idbanan); //ID hóa đơn vừa thêm vào lớn nhất
                        if (dataGridThucdon.CurrentRow != null && dataGridThucdon.SelectedRows.Count > 0) //Nếu đã chọn sản phẩm
                        {
                            int idsanpham = (int)dataGridThucdon.CurrentRow.Cells[0].Value;
                            int soluong = (int)numUDSoluong.Value;
                            if (soluong == 0) //Nếu chưa chọn số lượng sản phẩm
                            {
                                MessageBox.Show("Xin hãy chọn số lượng", "Thông báo");
                            }
                            else
                            {
                                ChiTietHoaDonDAO.Instance.ThemCTHD(HoaDonDAO.Instance.LayIdHoaDonLonNhat(), idsanpham, soluong);
                                LoadDanhSachBanAn();
                                ShowHoaDonChoBanAn(idbanan);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xin hãy chọn sản phẩm", "Thông báo");
                        }
                    }
                    else //Nếu đã tồn tại thì update lại chi tiết hóa đơn
                    {
                        if (dataGridThucdon.CurrentRow != null && dataGridThucdon.SelectedRows.Count > 0) //Nếu đã chọn sản phẩm
                        {
                            int idsanpham = (int)dataGridThucdon.CurrentRow.Cells[0].Value;
                            int soluong = (int)numUDSoluong.Value;
                            if (soluong == 0) //Nếu chưa chọn số lượng sản phẩm
                            {
                                MessageBox.Show("Xin hãy chọn số lượng", "Thông báo");
                            }
                            else
                            {
                                ChiTietHoaDonDAO.Instance.ThemCTHD(idhoadon, idsanpham, soluong);
                                LoadDanhSachBanAn();
                                ShowHoaDonChoBanAn(idbanan);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Xin hãy chọn sản phẩm", "Thông báo");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Xin hãy chọn bàn ăn", "Thông báo");
                }
            }
            else
            {
                if (dataGridThucdon.CurrentRow != null && dataGridThucdon.SelectedRows.Count > 0) //Nếu đã chọn sản phẩm
                {
                    int idhoadon = HoaDonDAO.Instance.LayIdHoaDonLonNhat();
                    int idsanpham = (int)dataGridThucdon.CurrentRow.Cells[0].Value;
                    int soluong = (int)numUDSoluong.Value;
                    int giatien = (int)dataGridThucdon.CurrentRow.Cells[4].Value;
                    if (soluong == 0) //Nếu chưa chọn số lượng sản phẩm
                    {
                        MessageBox.Show("Xin hãy chọn số lượng", "Thông báo");
                    }
                    else
                    {
                        ChiTietHoaDonDAO.Instance.ThemCTHD(idhoadon, idsanpham, soluong);
                        ShowHoaDonMangVe();
                        ShowThanhTienMangVe();
                    }
                }
                else
                {
                    MessageBox.Show("Xin hãy chọn sản phẩm", "Thông báo");
                }
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (radioAnTaiBan.Checked)
            {
                if (dgvBanan1.CurrentRow.Selected)
                {
                    int idbanan = (int)dgvBanan1.CurrentRow.Cells[0].Value;
                    int idhoadon = HoaDonDAO.Instance.LayHoaDonTheoBanAn(idbanan);
                    if (idhoadon != -1)
                    {
                        int tongTien = int.Parse(lblThanhTien.Text);
                        if (MessageBox.Show("Xác nhận thanh toán", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            HoaDonDAO.Instance.ThanhToanHoaDon(idhoadon, tongTien, idbanan);
                            ShowHoaDonChoBanAn(idbanan);
                            LoadDanhSachBanAn();
                            HienThiDanhSachHoaDon(HoaDonDAO.Instance.HienThiDanhSachHoaDon());
                            lblThanhTien.Text = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bàn ăn không có hóa đơn chưa thanh toán", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Xin hãy chọn bàn ăn", "Thông báo");
                }
            }
            else
            {
                int idhoadon = HoaDonDAO.Instance.LayIdHoaDonLonNhat();
                int tongtien = int.Parse(lblThanhTien.Text);
                if (MessageBox.Show("Xác nhận thanh toán", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    HoaDonDAO.Instance.ThanhToanHoaDonMangVe(idhoadon, tongtien);
                    HienThiDanhSachHoaDon(HoaDonDAO.Instance.HienThiDanhSachHoaDon());
                    lvHoaDon1.Clear();
                    lblThanhTien.Text = "0";
                }
            }
            
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            string tenBanAn = txtTenBan.Text;
            string trangthai = "Trống";
            BanAnDTO banan = new BanAnDTO(0, tenBanAn, trangthai);
            BanAnDAO.Instance.ThemBanAn(banan);
            LoadDanhSachBanAn();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtgrBanan2.CurrentRow != null && dtgrBanan2.SelectedRows.Count > 0)
            {
                int id = int.Parse(lblIDBan.Text);
                BanAnDAO.Instance.XoaBanAn(id);
                LoadDanhSachBanAn();
            }
            else
            {
                MessageBox.Show("Hãy chọn bàn ăn cần xóa", "Thông báo");
            }

        }

        private void btnSuaBan_Click(object sender, EventArgs e)
        {
            if (dtgrBanan2.CurrentRow != null && dtgrBanan2.SelectedRows.Count > 0)
            {
                string tenBanAn = txtTenBan.Text;
                string trangthai = "Trống";
                int id = int.Parse(lblIDBan.Text);
                BanAnDTO banan = new BanAnDTO(id, tenBanAn, trangthai);
                BanAnDAO.Instance.SuaBanAn(banan);
                LoadDanhSachBanAn();
            }
            else
            {
                MessageBox.Show("Hãy chọn bàn ăn cần sửa", "Thông báo");
            }
        }

        private void dtgrBanan2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgrBanan2.SelectedRows.Count > 0)
            {
                string tenBan = dtgrBanan2.CurrentRow.Cells[1].Value.ToString();
                string idban = dtgrBanan2.CurrentRow.Cells[0].Value.ToString();
                txtTenBan.Text = tenBan;
                lblIDBan.Text = idban;
            }
        }

        private void dtgrDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgrDanhMuc.CurrentRow.Selected)
            {
                int iddanhmuc = (int)dtgrDanhMuc.CurrentRow.Cells[0].Value;
                HienThiSanPhamTheoDanhMucTabThucAn(iddanhmuc);
                txtDanhMuc.Text = dtgrDanhMuc.CurrentRow.Cells[1].Value.ToString();
                lblIDDM.Text = dtgrDanhMuc.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtDanhMuc.Text;
            int iddanhmuc = int.Parse(lblIDDM.Text);
            DanhMucDTO danhMuc = new DanhMucDTO(0, tenDanhMuc);
            DanhMucDAO.Instance.ThemDanhMuc(danhMuc);
            LoadDanhMucTabThucAn();
            HienThiDanhSachDanhMuc();
        }

        private void btnXoaDanhMuc_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtDanhMuc.Text;
            int iddanhmuc = int.Parse(lblIDDM.Text);
            if (MessageBox.Show("Xác nhận xóa danh mục", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DanhMucDTO danhMuc = new DanhMucDTO(0, tenDanhMuc);
                DanhMucDAO.Instance.XoaDanhMuc(iddanhmuc);
                LoadDanhMucTabThucAn();
                HienThiDanhSachDanhMuc();
            }
        }

        private void dtgrDanhMuc_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtgrDanhMuc.Rows[e.RowIndex].Cells[1].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dtgrDanhMuc.DataSource];
                currencyManager.SuspendBinding();
                dtgrDanhMuc.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }

        private void dtgrMonAn_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dtgrMonAn.Rows[e.RowIndex].Cells[3].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dtgrMonAn.DataSource];
                currencyManager.SuspendBinding();
                dtgrMonAn.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }

        private void btnThemThucDon_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text;
            int donGia = (int)nmDonGia.Value;
            if (dtgrDanhMuc.CurrentRow.Selected)
            {
                int idDanhmuc = (int)dtgrDanhMuc.CurrentRow.Cells[0].Value;
                SanPhamDTO sanPham = new SanPhamDTO(0, idDanhmuc, 0, tenMon, donGia);
                SanPhamDAO.Instance.ThemSanPham(sanPham);
                HienThiSanPhamTheoDanhMucTabThucAn(idDanhmuc);
            }
            else
            {
                MessageBox.Show("Xin chọn danh mục sản phẩm", "Thông báo");
            }
            
        }

        private void dtgrMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgrMonAn.CurrentRow.Selected)
            {
                txtTenMon.Text = dtgrMonAn.CurrentRow.Cells[3].Value.ToString();
                nmDonGia.Value = decimal.Parse(dtgrMonAn.CurrentRow.Cells[4].Value.ToString());
                lblIDMon.Text = dtgrMonAn.CurrentRow.Cells[0].Value.ToString();
            }
        }

        private void btnXoaThucDon_Click(object sender, EventArgs e)
        {
            int idMon = int.Parse(lblIDMon.Text);
            if (dtgrDanhMuc.CurrentRow.Selected)
            {
                int idDanhmuc = (int)dtgrDanhMuc.CurrentRow.Cells[0].Value;
                if (MessageBox.Show("Xác nhận xóa món ăn", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    SanPhamDAO.Instance.XoaSanPham(idMon);
                    HienThiSanPhamTheoDanhMucTabThucAn(idDanhmuc);
                }
            }
                
        }

        private void btnSuaMon_Click(object sender, EventArgs e)
        {
            string tenMon = txtTenMon.Text;
            int donGia = (int)nmDonGia.Value;
            int idmon = int.Parse(lblIDMon.Text);
            if (dtgrDanhMuc.CurrentRow.Selected)
            {
                int idDanhmuc = (int)dtgrDanhMuc.CurrentRow.Cells[0].Value;
                SanPhamDTO sanPham = new SanPhamDTO(idmon, idDanhmuc, 0, tenMon, donGia);
                SanPhamDAO.Instance.SuaSanPham(sanPham);
                HienThiSanPhamTheoDanhMucTabThucAn(idDanhmuc);
            }
            else
            {
                MessageBox.Show("Xin chọn danh mục sản phẩm", "Thông báo");
            }
        }

        private void btnSuaDanhMuc_Click(object sender, EventArgs e)
        {
            string tenDanhMuc = txtDanhMuc.Text;
            int iddanhmuc = int.Parse(lblIDDM.Text);
            DanhMucDTO danhMuc = new DanhMucDTO(iddanhmuc, tenDanhMuc);
            DanhMucDAO.Instance.SuaDanhMuc(danhMuc);
            LoadDanhMucTabThucAn();
        }

        private void dataGridThucdon_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dataGridThucdon.Rows[e.RowIndex].Cells[3].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridThucdon.DataSource];
                currencyManager.SuspendBinding();
                dataGridThucdon.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }

        private void dgvBanan1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBanan1.CurrentRow.Selected)
            {
                int idban = (int)dgvBanan1.CurrentRow.Cells[0].Value;
                ShowThanhTienChoBanAn(idban);
            }
        }

        private void dataGridTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridTaiKhoan.CurrentRow.Selected)
            {
                lblTDTK.Text = dataGridTaiKhoan.CurrentRow.Cells[0].Value.ToString();
                txtTenDangNhap.Text = dataGridTaiKhoan.CurrentRow.Cells[1].Value.ToString();
                txtMK.Text = dataGridTaiKhoan.CurrentRow.Cells[2].Value.ToString();
                txtEmail.Text = dataGridTaiKhoan.CurrentRow.Cells[3].Value.ToString();
                txtHoTen.Text = dataGridTaiKhoan.CurrentRow.Cells[4].Value.ToString();
                if (dataGridTaiKhoan.CurrentRow.Cells[5].Value != null)
                {
                    txtNgaySinh.Text = dataGridTaiKhoan.CurrentRow.Cells[5].Value.ToString();
                }
                txtGioiTinh.Text = dataGridTaiKhoan.CurrentRow.Cells[6].Value.ToString();
                txtChucVu.Text = dataGridTaiKhoan.CurrentRow.Cells[7].Value.ToString();
                txtSDT.Text = dataGridTaiKhoan.CurrentRow.Cells[8].Value.ToString();
            }
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            string tendangnhap = txtTenDangNhap.Text;
            string matkhau = txtMK.Text;
            string email = txtEmail.Text;
            string hoten = txtHoTen.Text;
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.Text);
            string gioitinh = txtGioiTinh.Text;
            string chucvu = txtChucVu.Text;
            string sodienthoai = txtSDT.Text;

            TaiKhoanDTO taiKhoan = new TaiKhoanDTO(0,tendangnhap,matkhau,email,hoten,ngaySinh,gioitinh,chucvu,sodienthoai);
            TaiKhoanDAO.Instance.ThemTaiKhoan(taiKhoan);
            LoadTaiKhoan();
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            int idtaikhoan = int.Parse(lblTDTK.Text);
            string tendangnhap = txtTenDangNhap.Text;
            string matkhau = txtMK.Text;
            string email = txtEmail.Text;
            string hoten = txtHoTen.Text;
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.Text);
            string gioitinh = txtGioiTinh.Text;
            string chucvu = txtChucVu.Text;
            string sodienthoai = txtSDT.Text;

            TaiKhoanDTO taiKhoan = new TaiKhoanDTO(idtaikhoan, tendangnhap, matkhau, email, hoten, ngaySinh, gioitinh, chucvu, sodienthoai);
            TaiKhoanDAO.Instance.SuaTaiKhoan(taiKhoan);
            LoadTaiKhoan();
        }

        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            int idtaikhoan = int.Parse(lblTDTK.Text);
            TaiKhoanDAO.Instance.XoaTaiKhoan(idtaikhoan);
            LoadTaiKhoan();
        }

        private void dataGridTaiKhoan_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (dataGridTaiKhoan.Rows[e.RowIndex].Cells[4].Value.ToString().Equals("Deleted"))
            {
                CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridTaiKhoan.DataSource];
                currencyManager.SuspendBinding();
                dataGridTaiKhoan.Rows[e.RowIndex].Visible = false;
                currencyManager.ResumeBinding();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (radioTKNgay1.Checked)
            {
                if (cmbNgay.Text != "")
                {
                    string[] temp = cmbNgay.Text.ToString().Split(' ');
                    string ngay = temp[1] + "/";
                    string thang = DateTime.Now.Month.ToString() + "/";
                    string nam = DateTime.Now.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeTheoNgay(date);
                    return;
                }
                else
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo");
                }
            }

            if (radioTKThang1.Checked)
            {
                if (cmbThang.Text != "")
                {
                    string ngay = "1/";
                    string[] temp = cmbThang.Text.ToString().Split(' ');
                    string thang = temp[1] + "/";
                    string nam = DateTime.Now.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeTheoThang(date);
                    return;
                }
                else
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo");
                }
            }

            if (radioTKTuChon1.Checked)
            {
                if (datePick1.Value != null)
                {
                    string ngay = datePick1.Value.Day.ToString() + "/";
                    string thang = datePick1.Value.Month.ToString() + "/";
                    string nam = datePick1.Value.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeTuChon(date.Date);
                }
                else
                {
                    MessageBox.Show("Hãy chọn ngày cụ thể", "Thông báo");
                }
            }
            
        }

        private void radioTKNgay1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    cmbNgay.Enabled = true;
                    datePick1.Enabled = false;
                    cmbThang.Enabled = false;
                }
            }
        }

        private void radioTKThang1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    cmbThang.Enabled = true;
                    datePick1.Enabled = false;
                    cmbNgay.Enabled = false;
                }
            }
        }

        private void radioTKTuChon1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    datePick1.Enabled = true;
                    cmbThang.Enabled = false;
                    cmbNgay.Enabled = false;
                }
            }
        }

        private void radioMangDi_Click(object sender, EventArgs e)
        {
            HoaDonDAO.Instance.ThemHoaDonMangVe(); //ID hóa đơn vừa thêm vào lớn nhất
        }

        private void radioAnTaiBan_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dgvBanan1.Enabled = true;
                    grboxBanan.Enabled = true;
                }
            }
        }

        private void radioMangDi_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    dgvBanan1.Enabled = false;
                    grboxBanan.Enabled = false;
                }
            }
        }

        private void radioNgay2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    cmbNgay2.Enabled = true;
                    datePick2.Enabled = false;
                    cmbThang2.Enabled = false;
                }
            }
        }

        private void radioThang2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    cmbNgay2.Enabled = false;
                    datePick2.Enabled = false;
                    cmbThang2.Enabled = true;
                }
            }
        }

        private void radioTuChon2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    cmbNgay2.Enabled = false;
                    datePick2.Enabled = false;
                    cmbThang2.Enabled = true;
                }
            }
        }

        private void btnTKSP_Click(object sender, EventArgs e)
        {
            if (radioNgay2.Checked)
            {
                if (cmbNgay2.Text != "")
                {
                    string[] temp = cmbNgay2.Text.ToString().Split(' ');
                    string ngay = temp[1] + "/";
                    string thang = DateTime.Now.Month.ToString() + "/";
                    string nam = DateTime.Now.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeSanPhamTheoNgay(date);
                    return;
                }
                else
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo");
                }
            }

            if (radioThang2.Checked)
            {
                if (cmbThang2.Text != "")
                {
                    string ngay = "1/";
                    string[] temp = cmbThang2.Text.ToString().Split(' ');
                    string thang = temp[1] + "/";
                    string nam = DateTime.Now.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeSanPhamTheoThang(date);
                    return;
                }
                else
                {
                    MessageBox.Show("Hãy chọn tháng", "Thông báo");
                }
            }

            if (radioTuChon2.Checked)
            {
                if (datePick2.Value != null)
                {
                    string ngay = datePick2.Value.Day.ToString() + "/";
                    string thang = datePick2.Value.Month.ToString() + "/";
                    string nam = datePick2.Value.Year.ToString();
                    DateTime date = DateTime.Parse(ngay + thang + nam);

                    ThongKeSanPhamTuChon(date.Date);
                }
                else
                {
                    MessageBox.Show("Hãy chọn ngày cụ thể", "Thông báo");
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            List<HoaDonDTO> hoaDon = HoaDonDAO.Instance.HienThiDanhSachHoaDon();
            List<HoaDonDTO> sortByID = hoaDon.OrderBy(o => o.IdHoaDon).ToList();
            HienThiDanhSachHoaDon(sortByID);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            List<HoaDonDTO> hoaDon = HoaDonDAO.Instance.HienThiDanhSachHoaDon();
            List<HoaDonDTO> sortByTongTien = hoaDon.OrderBy(o => o.TongTien).ToList();
            HienThiDanhSachHoaDon(sortByTongTien);
        }
    }
}
