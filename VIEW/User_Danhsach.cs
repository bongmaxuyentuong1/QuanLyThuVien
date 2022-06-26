using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
using QuanLyThuVien.Entity;

namespace QuanLyThuVien.VIEW
{
    public partial class User_Danhsach : Form
    {
        private string manguoidung = null;
        public User_Danhsach()
        {
            InitializeComponent();
            GUI();
        }

        public void setDGVColumnsHeader()
        {
            dataGridView.Columns[0].HeaderText = "Mã người dùng";
            dataGridView.Columns[1].HeaderText = "Họ tên";
            dataGridView.Columns[2].HeaderText = "Năm sinh";
            dataGridView.Columns[3].HeaderText = "Giới tính";
            dataGridView.Columns[4].HeaderText = "Điện thoại";
            dataGridView.Columns[5].HeaderText = "Email";
            dataGridView.Columns[6].HeaderText = "Tài khoản";
            dataGridView.Columns[7].HeaderText = "Nhiệm vụ";
        }
        public User_Danhsach(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            dataGridView.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 1,
                TEXT = "Mã người dùng"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 2,
                TEXT = "Họ và tên"
            });
            cbbThuocTinh.Items.Add(new CBB_ITEM()
            {
                VALUE = 3,
                TEXT = "Năm sinh"
            });
            cbbThuocTinh.SelectedIndex = 0;
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 0, TEXT = "Tất cả" });
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 1, TEXT = "Admin" });
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 2, TEXT = "Thủ thư" });
            cbbNhiemVu.SelectedIndex = 0;
        }
        public void ShowDGV()
        {

            dataGridView.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
        }
        private void btnToanBo_Click(object sender, EventArgs e)
        {
            ShowDGV();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            User_Them user_Them = new User_Them(this.manguoidung);
            user_Them.d = new User_Them.Mydel(ShowDGV);
            user_Them.Show();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                string manguoidungcanchinhsua = dataGridView.SelectedRows[0].Cells["MANGUOIDUNG"].Value.ToString();
                User_Sua user_Sua = new User_Sua(manguoidung, manguoidungcanchinhsua);
                user_Sua.d = new User_Sua.Mydel(ShowDGV);
                user_Sua.Show();
                ShowDGV();
            }
            else
            {
                //MessageBox.Show("Vui long  chon 1 dong");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn 1 dòng để sửa!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                List<string> list_manguoidung = new List<string>();
                foreach (DataGridViewRow row in dataGridView.SelectedRows)
                {
                    list_manguoidung.Add(row.Cells["MANGUOIDUNG"].Value.ToString());
                }
                BLL_NGUOIDUNG.Instance.xoaNguoiDung(list_manguoidung);
                //dataGridView.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
                //setDGVColumnsHeader();
                ShowDGV();
            }
            else
            {
                //MessageBox.Show("chon dong de xoa");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string manguoidung = txtMaNguoiDung.Text;
            string hoten = txtTenNguoiDung.Text;
            string taikhoan = txtTaiKhoan.Text;
            int nhiemvu = ((CBB_ITEM)cbbNhiemVu.SelectedItem).VALUE;
            bool res = Int32.TryParse(txtNamSinh.Text, out int namsinh);
            if (!res)
            {
                namsinh = 0;
            }
            string dienthoai = txtDienThoai.Text;
            string email = txtEmail.Text;
            dataGridView.DataSource = typeof(List<NGUOIDUNG_VIEW>);
            dataGridView.DataSource = BLL_NGUOIDUNG.Instance.timNguoiDung(manguoidung, hoten, taikhoan, nhiemvu, namsinh, dienthoai, email);
            setDGVColumnsHeader();
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            int value = ((CBB_ITEM)cbbThuocTinh.SelectedItem).VALUE;
            List<NGUOIDUNG_VIEW> list = (List<NGUOIDUNG_VIEW>)dataGridView.DataSource;
            dataGridView.DataSource = typeof(List<NGUOIDUNG_VIEW>);
            dataGridView.DataSource = BLL_NGUOIDUNG.Instance.sapXepNguoiDung(list, value);
            setDGVColumnsHeader();
        }
    }
}
