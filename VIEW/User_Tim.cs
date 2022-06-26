using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.DTO;
using QuanLyThuVien.BLL;
using QuanLyThuVien.Entity;
namespace QuanLyThuVien.VIEW
{
    public partial class User_Tim : Form
    {
        public User_Tim()
        {
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã người dùng";
            dataGridView1.Columns[1].HeaderText = "Họ tên";
            dataGridView1.Columns[2].HeaderText = "Năm sinh";
            dataGridView1.Columns[3].HeaderText = "Giới tính";
            dataGridView1.Columns[4].HeaderText = "Điện thoại";
            dataGridView1.Columns[5].HeaderText = "Email";
            dataGridView1.Columns[6].HeaderText = "Tài khoản";
            dataGridView1.Columns[7].HeaderText = "Nhiệm vụ";
        }
        public void GUI()
        {
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 0, TEXT = "Tất cả" });
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 1, TEXT = "Admin" });
            cbbNhiemVu.Items.Add(new CBB_ITEM() { VALUE = 2, TEXT = "Thủ thư" });
            cbbNhiemVu.SelectedIndex = 0;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string manguoidung = txtMaND.Text;
            string hoten = txtTenND.Text;
            string taikhoan = txtTaikhoan.Text;
            int nhiemvu = ((CBB_ITEM)cbbNhiemVu.SelectedItem).VALUE;
            bool res = Int32.TryParse(txtNS.Text, out int namsinh);
            if (!res)
            {
                namsinh = 0;
            }
            string dienthoai = txtDienthoai.Text;
            string email = txtEmail.Text;
            dataGridView1.DataSource = typeof(List<NGUOIDUNG_VIEW>);
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.timNguoiDung(manguoidung, hoten, taikhoan, nhiemvu, namsinh, dienthoai, email);
            setDGVColumnsHeader();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string mand = dataGridView1.SelectedRows[0].Cells["MANGUOIDUNG"].Value.ToString();
                User user = new User(mand);
                user.Show();
            }
        }
    }
}
