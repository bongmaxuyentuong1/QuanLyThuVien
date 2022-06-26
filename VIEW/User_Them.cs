using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Entity;
using QuanLyThuVien.DTO;
using QuanLyThuVien.BLL;
namespace QuanLyThuVien.VIEW
{
    public partial class User_Them : Form
    {
        public delegate void Mydel();
        public Mydel d = null;
        private string manguoidung = null;

        QuanLyThuVienEntities entities = new QuanLyThuVienEntities();
        public User_Them()
        {
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
        }
        public User_Them(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
            setDGVColumnsHeader();
        }
        private void GUI()
        {
            lbMaNguoiDung.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_NGUOIDUNG.Instance.getAllMaNguoiDung()));
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
        private void btnThem_Click(object sender, EventArgs e)
        {
            string manguoidung = lbMaNguoiDung.Text;
            string hoten = txtHvt.Text;
            DateTime namsinh = dateTimePicker1.Value.Date;
            bool gender = rbNam.Checked;
            string dt = txtDienthoai.Text;
            string email = txtEmail.Text;
            string user = txtUser.Text;
            string pass = txtPass.Text;
            string manhiemvu = rbAdmin.Checked ? "001" : "002";
            try
            {
                BLL_NGUOIDUNG.Instance.themNguoiDung(manguoidung, hoten, namsinh, gender, dt, email, user, pass, manhiemvu);
                dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
                if (manguoidung != null && d != null)
                    d();
                reLoad();
            }
            catch (Exception ex)
            {
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Kiểm tra đầu vào!");
            }
        }

        private void butXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<string> list_manguoidung = new List<string>();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    list_manguoidung.Add(row.Cells["MANGUOIDUNG"].Value.ToString());
                }
                BLL_NGUOIDUNG.Instance.xoaNguoiDung(list_manguoidung);
                dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
                setDGVColumnsHeader();
                d();
            }
            else
            {
                //MessageBox.Show("Chon dong de xoa");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chọn dòng để xóa!");
            }
            dataGridView1.DataSource = BLL_NGUOIDUNG.Instance.getAllUserViews();
        }
        public void reLoad()
        {
            txtDienthoai.Text = "";
            txtEmail.Text = "";
            txtHvt.Text = "";
            txtPass.Text = "";
            txtUser.Text = "";
            lbMaNguoiDung.Text = BLL_ID.Instance.convertInt2Ma(BLL_ID.Instance.kiemTraViTriCoTheThem(BLL_NGUOIDUNG.Instance.getAllMaNguoiDung()));
        }
    }
}
