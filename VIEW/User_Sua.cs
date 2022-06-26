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
    public partial class User_Sua : Form
    {
        public delegate void Mydel();
        public Mydel d;
        private string manguoidung = null;
        private string manguoidungcanchinhsua = null;
        private NGUOIDUNG nguoidungcanchinhsua = null;
        public User_Sua(string manguoidung, string manguoidungcanchinhsua)
        {
            this.manguoidung = manguoidung;
            this.manguoidungcanchinhsua = manguoidungcanchinhsua;
            InitializeComponent();
            this.nguoidungcanchinhsua = BLL_NGUOIDUNG.Instance.timNguoiDungTheoMaNguoiDung(this.manguoidungcanchinhsua);
            GUI();
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
        }
        public void GUI()
        {
            lbMaNguoiDung.Text = this.nguoidungcanchinhsua.MANGUOIDUNG;
            txtHvt.Text = this.nguoidungcanchinhsua.HOTEN;
            txtUser.Text = this.nguoidungcanchinhsua.TAIKHOAN;
            txtDienthoai.Text = this.nguoidungcanchinhsua.DIENTHOAI;
            txtEmail.Text = this.nguoidungcanchinhsua.EMAIL;
            dateTimePicker1.Value = this.nguoidungcanchinhsua.NAMSINH;
            rbNam.Checked = this.nguoidungcanchinhsua.GIOITINH;
            rbNu.Checked = !this.nguoidungcanchinhsua.GIOITINH;
            rbAdmin.Checked = this.nguoidungcanchinhsua.MANHIEMVU == "001";
            rbManager.Checked = this.nguoidungcanchinhsua.MANHIEMVU == "002";
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
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string hoten = txtHvt.Text;
            string taikhoan = txtUser.Text;
            string dienthoai = txtDienthoai.Text;
            string email = txtEmail.Text;
            DateTime namsinh = dateTimePicker1.Value;
            bool gioitinh = rbNam.Checked;
            string manhiemvu = rbAdmin.Checked ? "001" : "002";
            BLL_NGUOIDUNG.Instance.suaNguoiDung(this.manguoidungcanchinhsua, hoten, taikhoan, dienthoai, email, namsinh, gioitinh, manhiemvu);
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice("Cập nhật thành công!");
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            d();
        }
    }
}
